using Spawners;
using UnityEngine;
using Zenject;
using States;

namespace Props.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        internal Vector3[] MovingSystem;
        internal Vector3 Position { get => transform.position; set => transform.position = value; }
        internal Vector3 Forward { get => transform.forward; set => transform.forward = value; }
        internal Transform Transform { get => transform; }
        private State _currentState;

        [Inject]
        private EnemyStatesSpawner _enemyStatesSpawner;

        private void Start()
        {
            ChangeState(EnemyStateTypes.FollowMovingSystemState);
            _currentState.Start();
        }

        private void Update()
        {
            _currentState.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentState.OnTriggerEnter(other);
        }

        private void ChangeState(EnemyStateTypes type)
        {
            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }

            _currentState = _enemyStatesSpawner.CreateState(type);
            _currentState.Start();
        }

        public class Factory : PlaceholderFactory<Enemy>
        {

        }
    }
}
