using System.Collections;
using UnityEngine;
using Zenject;
using Pool;

namespace Props.Chests
{
    internal class ChestAnimator : MonoBehaviour
    {
        private readonly int OpenIndex = Animator.StringToHash("Open");

        private float _openDuration;
        private Animator _animator;

        [Inject]
        private Chest _chest;

        [Inject]
        private PickUpPool _pickUpPool;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)                 
            {
                AnimationClip clip = ac.animationClips[i];
                if (clip.name == "Open")        
                {
                    _openDuration = clip.length;
                }
            }
        }

        internal void Open()
        {
            _animator.SetTrigger(OpenIndex);
            StartCoroutine(WaitToDestroyChest());
        }

        internal IEnumerator WaitToDestroyChest()
        {
            yield return new WaitForSeconds(_openDuration);
            Transform pickUpTransform = _pickUpPool.GetFreeElement();
            pickUpTransform.position = transform.position;
            Destroy(_chest.gameObject);
        }
    }
}
