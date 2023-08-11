using UnityEngine;
using DG.Tweening;
using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Interfaces;
using Utilities.Utils;

namespace UI
{
    internal class PickUpUIAnimation : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private Vector3 _aimPosition;

        private GameObject _gameObject;
        private Tween[] _tweens;

        private float _duration;
        private Image _image;

        private IResourceCounter _resourceCounter;

        [Inject]
        private void Construct(Settings settings)
        {
            _image = GetComponent<UnityEngine.UI.Image>();
            _duration = settings.Duration;
            _image.useSpriteMesh = true;
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

        internal void SetUpAnimation(Vector3 startPosition, Vector3 endPosition, Sprite sprite, IResourceCounter resourceCounter)
        {
            _resourceCounter = resourceCounter;
            _image.sprite = sprite;
            _rectTransform.position = startPosition;
            Tween positionTween = _rectTransform.DOMove(endPosition, _duration)
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
            public List<PickUpAnimationData> PickUpAnimationData;
        }
    }
}
