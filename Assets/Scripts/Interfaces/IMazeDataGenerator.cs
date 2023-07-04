using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    internal interface IMazeDataGenerator
    {
        public int[,] Maze { get; }
        public int[,] GenerateMazeData();
    }
}
