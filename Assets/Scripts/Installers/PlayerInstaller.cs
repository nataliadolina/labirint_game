using Props.Player;
using Spawners;
using States;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController;
    public override void InstallBindings()
    {
        Container.Bind<PlayerStatesSpawner>().AsSingle().NonLazy();
        Container.BindFactory<PlayerMove, PlayerMove.Factory>().WhenInjectedInto<PlayerStatesSpawner>();
        Container.BindInstance(playerAnimatorController).WhenInjectedInto<PlayerMove>();
    }
}