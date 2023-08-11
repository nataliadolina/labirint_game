using ModestTree;
using UnityEngine;
using Props.Walls;


namespace Spawners
{
    public enum WallType
    {
        MazeWall,
        MazeWallWithTourch,
    }

    internal class Spawner
    {
        private MazeWall.Factory _mazeWallFactory;
        private MazeWallWithTourch.Factory _mazeWallWithTourchFactory;

        private bool _withTorch = false;

        public Spawner(
            MazeWall.Factory mazeWallFactory,
            MazeWallWithTourch.Factory mazeWallWithTourchFactory)
        {
            _mazeWallFactory = mazeWallFactory;
            _mazeWallWithTourchFactory = mazeWallWithTourchFactory;
        }

        public Transform CreateObject()
        {
            WallType wallType = !_withTorch ? WallType.MazeWall : WallType.MazeWallWithTourch;
            _withTorch = !_withTorch;

            switch (wallType)
            {
                case WallType.MazeWall:
                    return _mazeWallFactory.Create().transform;

                case WallType.MazeWallWithTourch:
                    return _mazeWallWithTourchFactory.Create().transform;
            }

            throw Assert.CreateException();
        }
    }
}
