using System;
using UnityEngine;
using Zenject;
using Props.Chests;
using Props.Player;
using Props.Walls;
using Props.Enemies;
using Maze;
using Spawners;
using States;
using UI;

namespace Installers
{
    internal class GameInstaller : MonoInstaller
    {
        [Inject]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            Container.Bind<MazeDataGenerator>().AsSingle().NonLazy();
            Container.Bind<MazeConstructor>().AsSingle().NonLazy();
            Container.Bind<PositionCellConverter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TouchController>().AsSingle().NonLazy();

            InstallSpawner();
            InstallPlayer();
        }

        private void InstallSpawner()
        {
            Container.Bind<Spawner>().AsSingle().NonLazy();
            Container.BindFactory<Enemy, Enemy.Factory>()
                    .FromComponentInNewPrefab(_settings.EnemyPrefab)
                    .WithGameObjectName("Enemy")
                    .UnderTransformGroup("Maze/Enemies")
                    .WhenInjectedInto<Spawner>();
            Container.BindFactory<Chest, Chest.Factory>()
                    .FromComponentInNewPrefab(_settings.ChestPrefab)
                    .WithGameObjectName("Chest")
                    .UnderTransformGroup("Maze/Chests")
                    .WhenInjectedInto<Spawner>();
            Container.BindFactory<MazeWall, MazeWall.Factory>()
                    .FromComponentInNewPrefab(_settings.MazeWallPrefab)
                    .WithGameObjectName("Wall")
                    .UnderTransformGroup("Maze/Walls")
                    .WhenInjectedInto<Spawner>();
            Container.BindFactory<MazeWallWithTourch, MazeWallWithTourch.Factory>()
                    .FromComponentInNewPrefab(_settings.MazeWallWithTourchPrefab)
                    .WithGameObjectName("Wall")
                    .UnderTransformGroup("Maze/Walls")
                    .WhenInjectedInto<Spawner>();
        }

        private void InstallPlayer()
        {
            Container.Bind<Player>().FromComponentInNewPrefab(_settings.PlayerPrefab).AsSingle().NonLazy();
        }

        [Serializable]
        public class Settings
        {
            public GameObject EnemyPrefab;
            public GameObject PlayerPrefab;
            public GameObject ChestPrefab;
            public GameObject MazeWallPrefab;
            public GameObject MazeWallWithTourchPrefab;
        }
    }
}
