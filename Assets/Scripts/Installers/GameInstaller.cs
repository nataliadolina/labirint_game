using System;
using UnityEngine;
using Zenject;
using Props.Player;
using Props.Walls;
using Props.Enemies;
using Maze;
using Spawners;
using Test.Maze;
using UI;
using Props.Chests;
using Enums;

namespace Installers
{
    internal class GameInstaller : MonoInstaller
    {
        [Inject]
        private Settings _settings = null;
        [SerializeField]
        private Transform canvasTransform;

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
            Container.BindFactory<Chest, Chest.Factory>()
                .FromComponentInNewPrefab(_settings.ChestPrefab)
                .WithGameObjectName("Chest")
                .UnderTransformGroup("Maze/Chests")
                .WhenInjectedInto<ChestSpawner>();
        }

        private void InstallUI()
        {
            Container.Bind<PickUpUIAnimationSpawner>().AsSingle().NonLazy();
            Container.BindMemoryPool<CoinUIAnimation, CoinUIAnimation.Pool>()
                .FromComponentInNewPrefab(_settings.UICoinPrefab)
                .UnderTransform(canvasTransform);
            Container.BindMemoryPool<BombUIAnimation, BombUIAnimation.Pool>()
                .FromComponentInNewPrefab(_settings.UIBombPrefab)
                .UnderTransform(canvasTransform);
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
            public GameObject UIBombPrefab;
            public GameObject UICoinPrefab;
        }
    }
}
