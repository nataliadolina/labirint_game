using States;
using ModestTree;

namespace Spawners
{
    internal enum EnemyStateTypes
    {
        FollowMovingSystemState
    }

    internal class EnemyStatesSpawner
    {
        private FollowMovingSystemState.Factory _enemyFollowMovingSystemStateFactory;

        internal EnemyStatesSpawner(FollowMovingSystemState.Factory enemyFollowMovingSystemStateFactory)
        {
            _enemyFollowMovingSystemStateFactory = enemyFollowMovingSystemStateFactory;
        }

        public State CreateState(EnemyStateTypes enemyStateTypes)
        {
            switch (enemyStateTypes)
            {
                case EnemyStateTypes.FollowMovingSystemState:
                    return _enemyFollowMovingSystemStateFactory.Create();
            }

            throw Assert.CreateException();
        }
    }
}
