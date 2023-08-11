using Maze;
using System.Collections.Generic;
using Spawners;
using UnityEngine;
using Utilities.Extensions;
using Interfaces;

namespace Props.Enemies
{
    internal class EnemySpawner
    {
        private Enemy.Factory _enemyFactory;

        internal EnemySpawner(Enemy.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        internal void CreateEnemy(Vector3 position)
        {
            Enemy enemy = _enemyFactory.Create();
            enemy.Position = position; 
        }
    }
}
