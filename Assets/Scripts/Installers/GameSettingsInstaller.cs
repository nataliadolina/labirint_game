using States;
using System;
using Zenject;
using Maze;
using Test.Maze;
using UnityEngine;
using UI;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/new GameSettingsInstaller")]
    internal class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        internal GameInstaller.Settings GameSettings;

        [SerializeField]
        internal MazeSettings Maze;

        [SerializeField]
        internal PlayerSettings Player;

        [SerializeField]
        internal EnemySettings Enemy;

        [SerializeField]
        internal UISettings UiSettings;

        [Serializable]
        internal class PlayerSettings
        {
            public PlayerMove.Settings StateMoving;
        }

        [Serializable]
        internal class MazeSettings
        {
            public MazeDataGenerator.Settings MazeData;
            public MazeFromFileDataGenerator.Settings MazeFromFileData;
            public PositionCellConverter.Settings PositionCellConverter;
        }

        [Serializable]
        internal class EnemySettings
        {
            public FollowMovingSystemState.Settings FollowMovingSystemStateSettings;
        }

        [Serializable]
        internal class UISettings
        {
            public PickUpUIAnimation.Settings PickUpUIAnimationSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Maze.MazeData);
            Container.BindInstance(Maze.MazeFromFileData);
            Container.BindInstance(Maze.PositionCellConverter);
            Container.BindInstance(Player.StateMoving);
            Container.BindInstance(GameSettings);
            Container.BindInstance(Enemy.FollowMovingSystemStateSettings);
            Container.BindInstance(UiSettings.PickUpUIAnimationSettings);
        }
    }
}
