using ModestTree;
using UnityEngine;
using Props.Enemies;
using Props.Player;
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
        private Enemy.Factory _enemyFactory;
        private Chest.Factory _chestFactory;
        private MazeWall.Factory _mazeWallFactory;
        private MazeWallWithTourch.Factory _mazeWallWithTourchFactory;

        public Spawner(
            Enemy.Factory enemyFactory,
            Chest.Factory chestFactory,
            MazeWall.Factory mazeWallFactory,
            MazeWallWithTourch.Factory mazeWallWithTourchFactory)
        {
            _enemyFactory = enemyFactory;
            _chestFactory = chestFactory;
            _mazeWallFactory = mazeWallFactory;
            _mazeWallWithTourchFactory = mazeWallWithTourchFactory;
        }

        public Transform CreateObject(FactoryTypes factoryType)
        {
            switch (factoryType)
            {
                case FactoryTypes.Enemy:
                    return _enemyFactory.Create().transform;

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
