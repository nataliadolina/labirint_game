using Spawners;
using States;
using Zenject;
using Props.Enemies;
using UnityEngine;

internal class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private Enemy enemy;
    public override void InstallBindings()
    {
        Container.Bind<EnemyStatesSpawner>().AsSingle().NonLazy();
        Container.BindInstance(enemy);
        Container.BindFactory<FollowMovingSystemState, FollowMovingSystemState.Factory>().WhenInjectedInto<EnemyStatesSpawner>();
    }
}
