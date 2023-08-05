using UnityEngine;
using DG.Tweening;

namespace Test.UI
{
    internal class TestPickUpUIAnimation : MonoBehaviour
    {
        [SerializeField]
        private bool rewind = false;

        [SerializeField]
        private Transform _aimTransform;
        [SerializeField]
        private float _duration;

        private Vector3 _aimPosition;
        private RectTransform _rectTransform;
        private GameObject _gameObject;

        private Tween[] _tweens;

        private void Start()
        {

            _aimPosition = _aimTransform.position;
            _rectTransform = GetComponent<RectTransform>();
            _gameObject = _rectTransform.gameObject;
            
            Vector3 aimScale = _rectTransform.localScale * 1.5f;
            Tween positionTween = _rectTransform.DOMove(_aimPosition, _duration)
                .SetEase(Ease.InQuad)
                .OnStart(OnStart)
                .OnComplete(OnComplete)
                .SetAutoKill(false)
                .Pause();
            Tween scaleTween = _rectTransform.DOScale(aimScale, _duration / 2)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause();

            _tweens = new Tween[2] { positionTween, scaleTween};
        }

        private void OnComplete()
        {
            _gameObject.SetActive(false);
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

        private void Update()
        {
            if (rewind)
            {
                StartAnimation();
                rewind = false;
            }
        }
    }
}
