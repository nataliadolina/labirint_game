using States;
using System;
using Zenject;
using Maze;
using UnityEngine;

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
        

        [Serializable]
        internal class PlayerSettings
        {
            public PlayerMove.Settings StateMoving;
        }

        [Serializable]
        internal class MazeSettings
        {
            public MazeDataGenerator.Settings MazeData;
            public MazeConstructor.Settings MazeConstructor;
            public PositionCellConverter.Settings PositionCellConverter;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Maze.MazeData);
            Container.BindInstance(Maze.MazeConstructor);
            Container.BindInstance(Maze.PositionCellConverter);
            Container.BindInstance(Player.StateMoving);
            Container.BindInstance(GameSettings);
        }
    }
}
