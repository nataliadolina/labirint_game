using System;
using UnityEngine;
using UniRx;

namespace UI
{
    internal class PlayerDirectionInput : JoystickControllerBase
    {
        internal ReactiveProperty<Vector2> Direction = new ReactiveProperty<Vector2>();
        internal ReactiveProperty<float> SpeedRatio = new ReactiveProperty<float>();
        private protected override void UpdatePlayerDirection(in Vector2 direction)
        {
            Direction.Value = direction;
            SpeedRatio.Value = direction.sqrMagnitude;
        }
    }
}
