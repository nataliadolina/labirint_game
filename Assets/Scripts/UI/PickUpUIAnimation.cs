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
        

        [Inject]
        private void Construct(Settings settings, ResourceCounter resourceCounter)
        {
            float duration = settings.Duration;

            _aimPosition = resourceCounter.IconTransform.position;
            _resourceCounter = resourceCounter;

            _rectTransform = GetComponent<RectTransform>();
            _gameObject = _rectTransform.gameObject;

            Vector3 aimScale = _rectTransform.localScale * 1.5f;

            Tween positionTween = _rectTransform.DOMove(_aimPosition, duration)
                .SetEase(Ease.InQuad)
                .OnStart(OnStart)
                .OnComplete(OnComplete)
                .SetAutoKill(false)
                .Pause();
            Tween scaleTween = _rectTransform.DOScale(aimScale, duration / 2)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause();

            _tweens = new Tween[2] { positionTween, scaleTween };
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
