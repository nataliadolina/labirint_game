using UnityEngine;

namespace Props.Player
{
    internal class PlayerAnimatorController : MonoBehaviour
    {
        private readonly int SpeedIndex = Animator.StringToHash("Speed");
        private readonly int StartRunningIndex = Animator.StringToHash("Start running");
        private readonly int StopRunningIndex = Animator.StringToHash("Stop running");

        private Animator _animator;
        private bool _isRunning = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        internal void SetRunning(bool value)
        {
            if (value != _isRunning)
            {
                _animator.SetTrigger(value ? StartRunningIndex : StopRunningIndex);
                _isRunning = value;
            }
        }
    }
}
