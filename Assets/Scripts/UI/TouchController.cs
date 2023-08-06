using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Zenject;

namespace UI
{
    internal class TouchController : MonoBehaviour
    {
        [Inject]
        private PlayerDirectionInput _playerDirectionInput;

        [Inject]
        private void Construct()
        {
            this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => _playerDirectionInput.StartUpdateJoyctickDirection());

            this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => _playerDirectionInput.ResetJoystick());

            this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(x => _playerDirectionInput.SetKnobPosition());
        }
    }
}
