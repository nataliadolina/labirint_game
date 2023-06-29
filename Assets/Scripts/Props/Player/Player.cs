using UnityEngine;
using States;
using Zenject;
using Spawners;

namespace Props.Player
{
    internal class Player : MonoBehaviour
    {
        public Vector3 Position { get => transform.position; set => transform.position = value; }

        public Transform Transform { get => transform; }
        public Vector3 Forward { get => transform.forward; set => transform.forward = value; }

        public Rigidbody Rigidbody { get => _rigidbody; }

        private State _currentState;
        private Rigidbody _rigidbody;

        [Inject]
        private PlayerStatesSpawner _playerStatesSpawner;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            ChangeState(PlayerStateTypes.PlayerMove);
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

        private void ChangeState(PlayerStateTypes type)
        {
            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }

            _currentState = _playerStatesSpawner.CreateState(type);
            _currentState.Start();
        }
    }
}
