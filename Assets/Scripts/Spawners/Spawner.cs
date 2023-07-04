using ModestTree;
using UnityEngine;
using Props.Chests;
using Props.Walls;


namespace Spawners
{
    public enum FactoryTypes
    {
        Player,
        Chest,
        Enemy,
        MazeWall,
        MazeWallWithTourch,
    }

    internal class Spawner
    {
        private Chest.Factory _chestFactory;
        private MazeWall.Factory _mazeWallFactory;
        private MazeWallWithTourch.Factory _mazeWallWithTourchFactory;

        public Spawner(
            Chest.Factory chestFactory,
            MazeWall.Factory mazeWallFactory,
            MazeWallWithTourch.Factory mazeWallWithTourchFactory)
        {
            _chestFactory = chestFactory;
            _mazeWallFactory = mazeWallFactory;
            _mazeWallWithTourchFactory = mazeWallWithTourchFactory;
        }

        public Transform CreateObject(FactoryTypes factoryType)
        {
            switch (factoryType)
            {
                case FactoryTypes.Chest:
                    return _chestFactory.Create().transform;

                case FactoryTypes.MazeWall:
                    return _mazeWallFactory.Create().transform;

                case FactoryTypes.MazeWallWithTourch:
                    return _mazeWallWithTourchFactory.Create().transform;
            }

            throw Assert.CreateException();
        }
    }
}
