using UnityEngine;
using Zenject;

namespace UI
{
    internal class TouchController : ITickable
    {
        private bool _isUserTouchingScreen;
        private bool IsUserTouchingScreen
        {
            set
            {
                if (value == false && value == _isUserTouchingScreen)
                {
                    return;
                }

                if (value == true && value == _isUserTouchingScreen)
                {
                    playerDirectionInput.SetKnobPosition();
                    return;
                }

                _isUserTouchingScreen = value;

                if (value)
                {
                    playerDirectionInput.StartUpdateJoyctickDirection();
                    return;
                    

                }
                else if (!value)
                {
                    playerDirectionInput.ResetJoystick();
                    _isUserTouchingScreen = value;

                }
            }
        }

        public void Tick()
        {
            SetIsInputTouchDown();
            SetIsInputTouchUp();
            SetIsInputTouchStay();
        }


        private void SetIsInputTouchDown()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            IsUserTouchingScreen = true;
        }

        private void SetIsInputTouchUp()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            IsUserTouchingScreen = false;
        }

        private void SetIsInputTouchStay()
        {
            if (Input.GetMouseButton(0))
            {
                IsUserTouchingScreen = true;
            }
        }

        [Inject]
        private PlayerDirectionInput playerDirectionInput;

    }
}
