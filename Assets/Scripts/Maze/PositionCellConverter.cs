using System;
using UnityEngine;

namespace Maze
{
    internal class PositionCellConverter
    {
        public static Vector3 _mazeStart;
        public static float _labirintWallScaleSize;
        public PositionCellConverter(Settings settings)
        {
            _mazeStart = settings.MazeStart;
            _labirintWallScaleSize = settings.LabirintWallScaleSize;
        }

        public Vector2 ReturnObjectCell(Vector3 position)
        {
            int row = Mathf.RoundToInt((position.x - _mazeStart.x) / _labirintWallScaleSize);
            int col = Mathf.RoundToInt((position.z - _mazeStart.z) / _labirintWallScaleSize);
            return new Vector2(row, col);
        }
        public Vector3 ReturnPositionInMaze(Vector2 cell)
        {
            return new Vector3(_mazeStart.x + _labirintWallScaleSize * cell.x, _mazeStart.y, _mazeStart.z + _labirintWallScaleSize * cell.y);
        }

        [Serializable]
        public class Settings
        {
            public Vector3 MazeStart;
            public float LabirintWallScaleSize;
        }
    }
}
