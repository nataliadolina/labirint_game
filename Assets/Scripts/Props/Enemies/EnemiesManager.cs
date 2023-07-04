using Maze;
using System.Collections.Generic;
using Spawners;
using UnityEngine;
using Extensions;
using Interfaces;

namespace Props.Enemies
{
    internal class EnemiesManager
    {
        private IMazeDataGenerator _dataGenerator;
        private PositionCellConverter _cellConverter;
        private Enemy.Factory _enemyFactory;

        internal EnemiesManager(IMazeDataGenerator dataGenerator, Enemy.Factory enemyFactory)
        {
            _dataGenerator = dataGenerator;
            _enemyFactory = enemyFactory;
        }

        internal void CreateEnemy(Vector3 position)
        {
            Enemy enemy = _enemyFactory.Create();
            enemy.Position = position; 
        }
    }
}
