using System;
using UI;
using UnityEngine;
using Zenject;
using Props.Player;
using Maze;

namespace States
{
    internal class PlayerMove : State
    { 
        private float _speed;

        private Vector2 _lastDirection;
        private float _lastSpeedRatio = 0;

        private PlayerDirectionInput _directionInput;
        private FieldLimits _fieldLimits;

        private Player _player;

        private PlayerAnimatorController _playerAnimatorController;

        private Vector2 LastDirection
        { 
            get => _lastDirection;
            set
            { 
                if (_lastDirection == value || value == Vector2.zero)
                {
                    return;
                }

                _lastDirection = value;
                _player.Forward = new Vector3(value.x, 0, value.y);
            }
        }

        private float LastSpeedRatio
        {
            get => _lastSpeedRatio;
            set
            {
                RaycastHit hit;
                if (Physics.Raycast(_player.Position, _player.Forward, out hit, 93.1f))
                {
                    if (hit.distance <= 0.5f)
                    {
                        value = 0f;
                    }                    
                }
                
                Vector3 moveToPosition = _player.Position + _player.Forward * _speed * value * Time.deltaTime;
                _player.Position = _fieldLimits.ClampPlayerPosition(moveToPosition);

                if (_lastSpeedRatio != value)
                {
                    _lastSpeedRatio = value;
                    _playerAnimatorController.SetRunning(value == 0 ? false : true);
                } 
            }
        }

        internal PlayerMove(
            FieldLimits fieldLimits,
            Player player,
            PlayerDirectionInput directionInput,
            PlayerAnimatorController playerAnimatorController,
            Settings settings)
        {
            _fieldLimits = fieldLimits;
            _player = player;
            _directionInput = directionInput;
            directionInput.onPlayerDirectionInput += ApplyDirection;
            _speed = settings.Speed;
            _playerAnimatorController = playerAnimatorController;
        }

        private void ApplyDirection(Vector2 direction)
        {
            LastSpeedRatio = direction.sqrMagnitude;
            LastDirection = direction;
        }

        public override void Dispose()
        {
            _directionInput.onPlayerDirectionInput -= ApplyDirection;
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
