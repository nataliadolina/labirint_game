using UnityEngine;
using Zenject;
using Props.Chests;
using Props;

namespace Installers
{
    internal class ChestInstaller : MonoInstaller
    {
        [SerializeField]
        private ChestAnimator chestAnimator;
        [SerializeField]
        private CustomTransform customChestTransform;
        [SerializeField]
        private ChestGUI chestGUI;

        public override void InstallBindings()
        {
            Container.BindInstance(chestAnimator).AsSingle().NonLazy();
            Container.BindInstance(chestGUI).AsSingle().NonLazy();
            Container.BindInstance(customChestTransform).AsSingle().NonLazy();
        }
    }
}
