using UnityEngine;
using Zenject;
using Props.Chests;
using UI;

namespace Installers
{
    internal class ChestInstaller : MonoInstaller
    {
        [SerializeField]
        private Chest chest;
        [SerializeField]
        private ChestAnimator chestAnimator;
        [SerializeField]
        private OpenButtonInput openButtonInput;

        public override void InstallBindings()
        {
            Container.BindInstance(chest).AsSingle().NonLazy();
            Container.BindInstance(chestAnimator).AsSingle().NonLazy();
            Container.BindInstance(openButtonInput).AsSingle().NonLazy();
        }
    }
}
