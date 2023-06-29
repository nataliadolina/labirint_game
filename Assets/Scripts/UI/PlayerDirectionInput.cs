using System;
using UnityEngine;

namespace UI
{
    internal class PlayerDirectionInput : JoystickControllerBase
    {
        internal event Action<Vector2> onPlayerDirectionInput;
        private protected override void UpdatePlayerDirection(in Vector2 direction)
        {
            onPlayerDirectionInput?.Invoke(direction);
        }
    }
}
