using System.Collections;
using UnityEngine;
using Zenject;

namespace Props.Chests
{
    internal class ChestAnimator : MonoBehaviour
    {
        private readonly int OpenIndex = Animator.StringToHash("Open");

        private float _openDuration;
        private Animator _animator;

        [Inject]
        private Chest _chest;

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

            Material mat = GetComponentInChildren<Renderer>().material;
        }

        internal void Open()
        {
            _animator.SetTrigger(OpenIndex);
            StartCoroutine(WaitToDestroyChest());
        }

        internal IEnumerator WaitToDestroyChest()
        {
            yield return new WaitForSeconds(_openDuration);
            Destroy(_chest.gameObject);
        }
    }
}
