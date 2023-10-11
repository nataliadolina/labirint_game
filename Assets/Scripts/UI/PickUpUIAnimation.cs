using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Interfaces;
using System.Linq;
using Enums;
using Spawners;
using Zenject;

namespace UI
{
    public abstract class PickUpUIAnimation : MonoBehaviour
    {
        protected abstract PickUpType pickUpType { get; }

        private RectTransform _rectTransform;

        private GameObject _gameObject;
        private Tween[] _tweens;

        private float _duration;

        private IResourceCounter _resourceCounter;
        private PickUpUIAnimationSpawner _pickUpUIAnimationSpawner;

        [Inject]
        private void Construct(Settings settings,
            IResourceCounter[] resourceCounters,
            PickUpUIAnimationSpawner spawner
            )
        {
            _duration = settings.Duration;
            _rectTransform = GetComponent<RectTransform>();
            _gameObject = _rectTransform.gameObject;
            _pickUpUIAnimationSpawner = spawner;

            Vector3 aimScale = _rectTransform.localScale * 1.5f;
            IResourceCounter resourceCounter = resourceCounters.Where(x => x.PickUpType == pickUpType).FirstOrDefault();
            Vector3 endPosition = _resourceCounter.IconPosition;

            Tween scaleTween = _rectTransform.DOScale(aimScale, _duration / 2)
                .SetLoops(2, LoopType.Yoyo)
                .OnStart(OnStart)
                .OnComplete(OnComplete)
                .SetAutoKill(false)
                .Pause();
            Tween positionTween = _rectTransform.DOMove(endPosition, _duration)
                .SetEase(Ease.InQuad)
                .SetAutoKill(false)
                .Pause();

            _tweens = new Tween[2] { scaleTween, positionTween };
        }

        private void OnComplete()
        {
            _gameObject.SetActive(false);
            _resourceCounter.AddResource();
            _pickUpUIAnimationSpawner.Despawn(pickUpType, this);
        }

        private void OnStart()
        {
            _gameObject.SetActive(true);
        }

        public void StartAnimation(Vector3  position)
        {
            transform.position = position;

            DOTween.RewindAll(_rectTransform);
            foreach (var tween in _tweens)
            {
                tween.TogglePause();
            }
        }

        [Serializable]
        public class Settings
        {
            public float Duration;
        }
    }
}
