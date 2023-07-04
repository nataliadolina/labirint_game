using UnityEngine;
using System.IO;
using System.Linq;
using Interfaces;
using System;
using TreeEditor;

namespace Test.Maze
{
    internal class MazeFromFileDataGenerator : IMazeDataGenerator
    {
        private int[,] _maze;
        private string _pathToFile;

        public int[,] Maze => _maze;

        internal MazeFromFileDataGenerator(Settings settings)
        {
            _pathToFile = settings.PathToFile;
        }

        public int[,] GenerateMazeData()
        {
            var lines = File.ReadAllLines(_pathToFile);

            int dim0 = lines.Length;
            int[] cells = lines[0].Split(',').Select(x => int.Parse(x)).ToArray();
            int dim1 = cells.Length;

            _maze = new int[dim0, dim1];

            for (int i = 0; i < dim1; i++)
            {
                _maze[0, i] = cells[i];
            }

            for (int i = 1; i < dim0; i++)
            {
                int[] cells1 = lines[i].Split(',').Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < dim1; j++)
                {
                    _maze[i, j] = cells1[j];
                }
                
            }
            return _maze;
        }

        [Serializable]
        internal class Settings
        {
            public string PathToFile;
        }
    }
}
