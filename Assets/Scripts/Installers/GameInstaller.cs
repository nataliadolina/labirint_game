using System;
using UnityEngine;
using Zenject;
using Props.Chests;
using Props.Player;
using Props.Walls;
using Props.Enemies;
using Maze;
using Spawners;
using UI;
using Pool;
using Test.Maze;
using Enums;

namespace Installers
{
    internal class GameInstaller : MonoInstaller
    {
        [Inject]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            switch (_settings.Test)
            {
                case false:
                    Container.BindInterfacesAndSelfTo<MazeDataGenerator>().AsSingle().NonLazy();
                    break;
                case true:
                    Container.BindInterfacesAndSelfTo<MazeFromFileDataGenerator>().AsSingle().NonLazy();
                    break;
            }
            
            Container.Bind<MazeConstructor>().AsSingle().NonLazy();

            Container.Bind<PathGenerator>().AsSingle().NonLazy();
            Container.Bind<PositionCellConverter>().AsSingle().NonLazy();

            InstallEnemies();
            InstallChests();
            InstallSpawner();
            InstallPlayer();
            InstallUI();
        }

        private void InstallEnemies()
        {
            Container.Bind<EnemySpawner>().AsSingle().NonLazy();
            Container.BindFactory<Enemy, Enemy.Factory>()
                    .FromComponentInNewPrefab(_settings.EnemyPrefab)
                    .UnderTransformGroup("Maze/Enemies")
                    .WhenInjectedInto<EnemySpawner>();
        }

        private void InstallUI()
        {
            Container.Bind<PickUpPool>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PickUpUISpawner>().AsSingle().NonLazy();
            Container.BindFactory<PickUpUIAnimation, PickUpUIAnimation.Factory>()
                .FromComponentInNewPrefab(_settings.CoinPrefab)
                .WithGameObjectName("Coin")
                .UnderTransformGroup("Canvas")
                .WhenInjectedInto<PickUpUISpawner>();
        }

        private void InstallSpawner()
        {
            Container.Bind<Spawner>().AsSingle().NonLazy();
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

        private void InstallChests()
        {
            Container.Bind<ChestSpawner>().AsSingle().NonLazy();
            Container.BindFactory<PickUpUIAnimationLauncher, PickUpUIAnimationLauncher.Factory>()
                .FromComponentInNewPrefab(_settings.ChestPrefab)
                .WithGameObjectName("Chest")
                .UnderTransformGroup("Maze/Chests")
                .WhenInjectedInto<ChestSpawner>();
        }

        [Serializable]
        public class Settings
        {
            public bool Test;

            public GameObject EnemyPrefab;
            public GameObject PlayerPrefab;
            public GameObject ChestPrefab;
            public GameObject MazeWallPrefab;
            public GameObject MazeWallWithTourchPrefab;

            public GameObject CoinPrefab;
        }
    }
}
