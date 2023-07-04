using System;
using System.Collections.Generic;
using Interfaces;

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

        private int GenerateIndex(List<int> generated, int minIndex, int maxIndex)
        {
            int index = UnityEngine.Random.Range(minIndex, maxIndex);
            while (generated.Contains(index))
            {
                index = UnityEngine.Random.Range(0, maxIndex);
            }
            return index;
        }
        private void GenerateChestsPos(int[,] maze)
        {
            List<int> generated = new List<int>();
            for (int i = 0; i < _chestsNumber; i++)
            {
                var index = GenerateIndex(generated, 1, emptyCells.Count);
                var cell = emptyCells[index];
                maze[cell.Item1, cell.Item2] = (int)MazeSigns.Chest;
                generated.Add(index);
            }
        }
        private void GenerateEnemiesPosts(int[,] maze)
        {
            List<int> generated = new List<int>();
            for (int i = 0; i < _enemiesNumber; i++)
            {
                var index = GenerateIndex(generated, 1, emptyCells.Count);
                var cell = emptyCells[index];
                maze[cell.Item1, cell.Item2] = (int)MazeSigns.Enemy;
                generated.Add(index);
            }
        }
        private void GeneratePlayerPos(int[,] maze)
        {
            for (int row = 0; row < maze.GetUpperBound(0); row++)
            {
                for (int col = 0; col < maze.GetUpperBound(1); col++)
                {
                    if (maze[row, col] == 0)
                    {
                        maze[row, col] = (int)MazeSigns.Player;
                        return;
                    }
                }
            }
        }

        public int[,] GenerateMazeData()
        {
            int[,] maze = new int[_sizeRows, _sizeCols];
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                    {
                        maze[i, j] = 0;
                    }
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (UnityEngine.Random.value > placementThreshold)
                        {
                            maze[i, j] = (int)MazeSigns.Wall;

                            int a = UnityEngine.Random.value < .5 ? (int)MazeSigns.EmptySpace : 1;
                            int b = a != 0 ? (int)MazeSigns.EmptySpace : (UnityEngine.Random.value < .5 ? -1 : 1);
                            maze[i + a, j + b] = (int)MazeSigns.Wall;
                        }
                    }
                    if (maze[i, j] == 0)
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            GenerateChestsPos(maze);
            GeneratePlayerPos(maze);
            GenerateEnemiesPosts(maze);
            _maze = maze;
            return maze;
        }

        [Serializable]
        public class Settings
        {
            public int LabirintSizeX;
            public int LabirintSizeY;

            public int ChestsNumber;
            public int EnemiesNumber;
            public float PlacementThreshold;
        }
    }
}