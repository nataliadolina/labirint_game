using UnityEngine;
using System;
using Spawners;
using Installers;
using Props.Player;

namespace Maze
{
    internal class MazeConstructor
    {
        [SerializeField]
        private bool showDebug;

        private Player _player;
        private MazeDataGenerator _dataGenerator;
        private Spawner _spawner;
        private PositionCellConverter _positionCellConverter;

        private int _labirintSizeX;
        private int _labirintSizeY;

        private float _wallHalfScaleY;
        private float _playerHalfScaleY;
        private float _chestHalfScaleY;
        private float _enemyHalfScaleY;

        private int[,] _data;

        public int[,] Data { get => _data; }

        internal MazeConstructor(
            MazeDataGenerator dataGenerator,
            Spawner spawner,
            PositionCellConverter positionCellConverter,
            Settings settings,
            GameInstaller.Settings gameInstallerSettings,
            Player player)
        {
            _player = player;
            _dataGenerator = dataGenerator;
            _spawner = spawner;
            _positionCellConverter = positionCellConverter;

            _labirintSizeX = settings.LabirintSizeX;
            _labirintSizeY = settings.LabirintSizeY;

            _wallHalfScaleY = gameInstallerSettings.MazeWallPrefab.transform.localScale.y / 2;
            _chestHalfScaleY = gameInstallerSettings.ChestPrefab.transform.localScale.y / 2;
            _playerHalfScaleY = gameInstallerSettings.PlayerPrefab.transform.localScale.y / 2;
            _enemyHalfScaleY = gameInstallerSettings.EnemyPrefab.transform.localScale.y / 2;

            GenerateNewMaze();
        }

        private void BuildMaze()
        {
            bool withTorch = false;
            for (int row = 0; row < _data.GetUpperBound(0); row++)
            {
                for (int col = 0; col < _data.GetUpperBound(1); col++)
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
                            break;

                        case (int)MazeSigns.Chest:
                            cellPosition.y = _chestHalfScaleY;
                            spawnedTransform = _spawner.CreateObject(FactoryTypes.Chest);
                            break;

                        case (int)MazeSigns.Enemy:
                            cellPosition.y = _enemyHalfScaleY;
                            spawnedTransform = _spawner.CreateObject(FactoryTypes.Enemy);
                            break;
                    }

                    spawnedTransform.position = cellPosition;
                }
            }
        }

        public void GenerateNewMaze()
        {
            _data = _dataGenerator.FromDimensions(_labirintSizeX, _labirintSizeY);
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

        [Serializable]
        public class Settings
        {
            public int LabirintSizeX;
            public int LabirintSizeY;
        }
    }
}
