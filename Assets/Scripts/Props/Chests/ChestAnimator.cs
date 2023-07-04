using UnityEngine;

namespace Props.Chests
{
    internal class ChestAnimator : MonoBehaviour
    {
        private readonly int OpenIndex = Animator.StringToHash("Open");
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        internal void Open()
        {
            _animator.SetTrigger(OpenIndex);
        }
    }
}
