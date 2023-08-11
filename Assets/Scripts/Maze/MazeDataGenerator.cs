using System;
using System.Collections.Generic;
using Interfaces;
using Utilities.Utils;

namespace Maze
{
    internal enum MazeSigns
    {
        Enemy = -4,
        Player = -3,
        Chest = -2,
        Wall = -1,
        EmptySpace = 0,
    }

    internal class MazeDataGenerator : IMazeDataGenerator
    {
        public float placementThreshold;
        private List<(int, int)> emptyCells;

        private int _chestsNumber;
        private int _enemiesNumber;
        private int _sizeRows;
        private int _sizeCols;

        private List<int> _generated = new List<int>();

        private int[,] _maze;

        public int[,] Maze { get => _maze; }

        internal MazeDataGenerator(Settings settings)
        {
            placementThreshold = settings.PlacementThreshold;
            emptyCells = new List<(int, int)>();

            _chestsNumber = settings.ChestsNumber;

            _enemiesNumber = settings.EnemiesNumber;

            _sizeRows = settings.LabirintSizeX;
            _sizeCols = settings.LabirintSizeY;
        }

        private int GenerateIndex(int minIndex, int maxIndex)
        {
            int index = UnityEngine.Random.Range(minIndex, maxIndex);
            while (_generated.Contains(index))
            {
                index = UnityEngine.Random.Range(0, maxIndex);
            }

            _generated.Add(index);
            return index;
        }

        private void GeneratePoses(MazeSigns sign, int number)
        {
            for (int i = 0; i < number; i++)
            {
                var index = GenerateIndex(1, emptyCells.Count);
                var cell = emptyCells[index];
                _maze[cell.Item1, cell.Item2] = (int)sign;
            }
        }

        private void GeneratePlayerPos()
        {
            var cell = emptyCells[0];
            _generated.Add(0);
            _maze[cell.Item1, cell.Item2] = (int)MazeSigns.Player;
        }

        public int[,] GenerateMazeData()
        {
            _maze = new int[_sizeRows, _sizeCols];
            int rMax = _maze.GetUpperBound(0);
            int cMax = _maze.GetUpperBound(1);

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                    {
                        _maze[i, j] = 0;
                    }
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (UnityEngine.Random.value > placementThreshold)
                        {
                            _maze[i, j] = (int)MazeSigns.Wall;

                            int a = UnityEngine.Random.value < .5 ? (int)MazeSigns.EmptySpace : 1;
                            int b = a != 0 ? (int)MazeSigns.EmptySpace : (UnityEngine.Random.value < .5 ? -1 : 1);
                            _maze[i + a, j + b] = (int)MazeSigns.Wall;
                        }
                    }
                    if (_maze[i, j] == 0)
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            GeneratePlayerPos();
            GeneratePoses(MazeSigns.Chest, _chestsNumber);
            GeneratePoses(MazeSigns.Enemy, _enemiesNumber);
            return _maze;
        }

        [Serializable]
        public class Settings
        {
            public int LabirintSizeX;
            public int LabirintSizeY;

            public int ChestsNumber;
            public int EnemiesNumber;

            public List<PickUpTypeAmountData> PickUpAmount;

            public float PlacementThreshold;
        }
    }
}