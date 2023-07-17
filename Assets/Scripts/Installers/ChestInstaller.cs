using UnityEngine;
using Zenject;
using Props.Chests;
using Props;
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
        private CustomTransform customChestTransform;
        [SerializeField]
        private ChestGUI chestGUI;

        public override void InstallBindings()
        {
            Container.BindInstance(chest).AsSingle().NonLazy();
            Container.BindInstance(chestAnimator).AsSingle().NonLazy();
            Container.BindInstance(chestGUI).AsSingle().NonLazy();
            Container.BindInstance(customChestTransform).AsSingle().NonLazy();
        }
    }
}
