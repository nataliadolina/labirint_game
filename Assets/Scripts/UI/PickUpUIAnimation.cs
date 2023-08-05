using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System;


namespace UI
{
    internal class PickUpUIAnimation : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private Vector3 _aimPosition;

        private GameObject _gameObject;
        private Tween[] _tweens;

        private ResourceCounter _resourceCounter;

        private float _duration;
        

        [Inject]
        private void Construct(Settings settings, ResourceCounter resourceCounter)
        {
            _duration = settings.Duration;

            _aimPosition = resourceCounter.IconTransform.position;
            _resourceCounter = resourceCounter;

            _rectTransform = GetComponent<RectTransform>();
            _gameObject = _rectTransform.gameObject;

            Vector3 aimScale = _rectTransform.localScale * 1.5f;

            
            Tween scaleTween = _rectTransform.DOScale(aimScale, _duration / 2)
                .SetLoops(2, LoopType.Yoyo)
                .OnStart(OnStart)
                .OnComplete(OnComplete)
                .SetAutoKill(false)
                .Pause();

            _tweens = new Tween[2] { scaleTween, null };
        }

        internal void SetFirstPathPoint(Vector3 startPosition)
        {
            _rectTransform.position = startPosition;
            Tween positionTween = _rectTransform.DOMove(_aimPosition, _duration)
                .SetEase(Ease.InQuad)
                .SetAutoKill(false)
                .Pause();
            _tweens[1] = positionTween;
        }

        private void OnComplete()
        {
            _gameObject.SetActive(false);
            _resourceCounter.AddResource();
        }

        private void OnStart()
        {
            _gameObject.SetActive(true);
        }

        public void StartAnimation()
        {
            DOTween.RewindAll(_rectTransform);
            foreach (var tween in _tweens)
            {
                tween.TogglePause();
            }
        }

        public class Factory : PlaceholderFactory<PickUpUIAnimation>
        {

        }

        [Serializable]
        public class Settings
        {
            public float Duration;
        }
    }
}
