using UnityEngine;
using System;
using Spawners;
using Installers;
using Props.Player;
using Props.Enemies;
using Interfaces;

namespace Maze
{
    internal class MazeConstructor
    {
        [SerializeField]
        private bool showDebug;

        private Player _player;
        private IMazeDataGenerator _dataGenerator;
        private Spawner _spawner;
        private PositionCellConverter _positionCellConverter;
        private EnemiesManager _enemiesManager;

        private float _wallHalfScaleY;
        private float _playerHalfScaleY;
        private float _chestHalfScaleY;
        private float _enemyHalfScaleY;

        private int[,] _data;

        internal MazeConstructor(
            IMazeDataGenerator dataGenerator,
            Spawner spawner,
            PositionCellConverter positionCellConverter,
            GameInstaller.Settings gameInstallerSettings,
            EnemiesManager enemiesManager,
            Player player)
        {
            _player = player;
            _dataGenerator = dataGenerator;
            _spawner = spawner;
            _enemiesManager = enemiesManager;
            _positionCellConverter = positionCellConverter;

            _wallHalfScaleY = gameInstallerSettings.MazeWallPrefab.transform.localScale.y / 2;
            _chestHalfScaleY = gameInstallerSettings.ChestPrefab.transform.localScale.y / 2;
            _playerHalfScaleY = gameInstallerSettings.PlayerPrefab.transform.localScale.y / 2;
            _enemyHalfScaleY = gameInstallerSettings.EnemyPrefab.transform.localScale.y / 2;

            GenerateNewMaze();
        }

        private void BuildMaze()
        {
            bool withTorch = false;
            for (int row = 0; row <= _data.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= _data.GetUpperBound(1); col++)
                {
                    int sign = _data[row, col];
                    if (sign == (int)MazeSigns.EmptySpace)
                    {
                        continue;
                    }

                    Vector3 cellPosition = _positionCellConverter.ReturnPositionInMaze(new Vector2(row, col));

                    if (sign == (int)MazeSigns.Player)
                    {
                        cellPosition.y = _playerHalfScaleY;
                        _player.Position = cellPosition;
                        continue;
                    }

                    Transform spawnedTransform = null;
                    switch (sign)
                    {
                        case (int)MazeSigns.Wall:
                            cellPosition.y = _wallHalfScaleY; 
                            FactoryTypes factoryType = !withTorch ? FactoryTypes.MazeWall : FactoryTypes.MazeWallWithTourch;
                            withTorch = !withTorch;
                            spawnedTransform = _spawner.CreateObject(factoryType);
                            spawnedTransform.position = cellPosition;
                            break;

                        case (int)MazeSigns.Chest:
                            cellPosition.y = _chestHalfScaleY;
                            spawnedTransform = _spawner.CreateObject(FactoryTypes.Chest);
                            spawnedTransform.position = cellPosition;
                            break;

                        case (int)MazeSigns.Enemy:
                            cellPosition.y = _enemyHalfScaleY;
                            _enemiesManager.CreateEnemy(cellPosition);
                            break;
                    }
                }
            }
        }

        public void GenerateNewMaze()
        {
            _data = _dataGenerator.GenerateMazeData();
            BuildMaze();
        }

#region OnGui

        private void OnGUI()
        {
            if (!showDebug)
            {
                return;
            }

            int[,] maze = _data;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            string msg = "";

            for (int i = rMax; i >= 0; i--)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == 0)
                    {
                        msg += "....";
                    }
                    else
                    {
                        msg += "==";
                    }
                }
                msg += "\n";
            }

            GUI.Label(new Rect(20, 20, 500, 500), msg);
        }

#endregion
    }
}
