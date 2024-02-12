using System.Collections;
using UnityEngine;
using Zenject;
using Enums;
using Spawners;
using UI;

namespace Props.Chests
{
    public class ChestAnimator : MonoBehaviour
    {
        private readonly int OpenIndex = Animator.StringToHash("Open");

        private float _openDuration;
        private Animator _animator;
        private PickUpType _pickUpType;

        [Inject]
        private CustomTransform _chestTransform;

        [Inject]
        private PickUpUIAnimationSpawner _pickUpUIAnimationSpawner;

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

        public void SetUp(PickUpType pickUpType)
        {
            _pickUpType = pickUpType;
        }

        public void Open()
        {
            _animator.SetTrigger(OpenIndex);
            StartCoroutine(WaitToDestroyChest());
        }

        internal IEnumerator WaitToDestroyChest()
        {
            yield return new WaitForSeconds(_openDuration);

            if (_pickUpType != PickUpType.None)
            {
                PickUpUIAnimation pickUpUIAnimation = _pickUpUIAnimationSpawner.Spawn(_pickUpType);
                pickUpUIAnimation.StartAnimation(Camera.main.WorldToScreenPoint(transform.position));
            } 
            
            Destroy(_chestTransform.GameObject);
        }
    }
}
