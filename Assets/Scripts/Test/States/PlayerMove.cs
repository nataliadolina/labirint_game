using System;
using UI;
using UnityEngine;
using Zenject;
using Props.Player;
using Maze;
using UniRx;

namespace States
{
    internal class PlayerMove : State
    { 
        private float _speed;
        private FieldLimits _fieldLimits;

        private Player _player;
        private PlayerAnimatorController _playerAnimatorController;

        private ReactiveProperty<float> _lastSpeedRatio = new ReactiveProperty<float>(0);

        private ReactiveProperty<Vector2> _lastDirection = new ReactiveProperty<Vector2>();

        internal PlayerMove(
            FieldLimits fieldLimits,
            Player player,
            PlayerDirectionInput directionInput,
            PlayerAnimatorController playerAnimatorController,
            Settings settings)
        {
            _fieldLimits = fieldLimits;
            _player = player;
            
            _speed = settings.Speed;
            _playerAnimatorController = playerAnimatorController;

            CreateSubscribions(directionInput);
        }

        private void CreateSubscribions(PlayerDirectionInput directionInput)
        {
            directionInput.Direction
                .Where(direction => _lastDirection.Value != direction && direction != Vector2.zero)
                .Subscribe(direction => _lastDirection.Value = direction);
            directionInput.SpeedRatio
                .Subscribe(speedRatio => _lastSpeedRatio.Value = speedRatio);

            _lastSpeedRatio.Subscribe(speedRatio => _playerAnimatorController.SetRunning(speedRatio == 0 ? false : true));
            _lastDirection
                .Where(x => x != Vector2.zero)
                .Subscribe(direction => _player.Forward = new Vector3(direction.x, 0, direction.y));
        }

        private void Move(float speedRatio)
        {
            Vector3 moveToPosition = _player.Position + _player.Forward * _speed * speedRatio * Time.deltaTime;
            _player.Position = _fieldLimits.ClampPlayerPosition(moveToPosition);
        }

        public override void Update()
        {
            if (!IsForwardAnyObstacles())
            {
                Move(_lastSpeedRatio.Value);
            }
        }

        private bool IsForwardAnyObstacles()
        {
            RaycastHit hit;
            if (Physics.Raycast(_player.Position, _player.Forward, out hit, 93.1f))
            {
                if (hit.distance <= 0.5f)
                {
                    return true;
                }
            }
            return false;
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }

        public class Factory : PlaceholderFactory<PlayerMove>
        {

        }
    }
}
