using UnityEngine;
using TMPro;
using DG.Tweening;

namespace UI
{
    internal class ResourceCounter : MonoBehaviour
    {
        [SerializeField]
        private Transform iconTransform;
        [SerializeField]
        private TMP_Text resourceAmountText;

        private int _currentResourceAmount = 0;
        private Tween _iconTween;

        internal Transform IconTransform => iconTransform;

        private void Start()
        {
            resourceAmountText.text = "0";
            _iconTween = iconTransform.DOScale(iconTransform.localScale * 1.5f, 0.5f).SetLoops(2, LoopType.Yoyo).Pause();
        }

        internal void AddResource()
        {
            _currentResourceAmount++;
            resourceAmountText.text = _currentResourceAmount.ToString();
            _iconTween.TogglePause();
        }
    }
}
