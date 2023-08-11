using UnityEngine;
using TMPro;
using DG.Tweening;
using Interfaces;
using Enums;

namespace UI
{
    internal class ResourceCounter : MonoBehaviour, IResourceCounter
    {
        [SerializeField]
        private RectTransform iconTransform;
        [SerializeField]
        private TMP_Text resourceAmountText;
        [SerializeField]
        private PickUpType pickUpType;

        private int _currentResourceAmount = 0;
        private Tween _iconTween;

        public Vector3 IconPosition => iconTransform.position;
        public PickUpType PickUpType { get => pickUpType; }

        private void Start()
        {
            resourceAmountText.text = "0";
            _iconTween = iconTransform
                .DOScale(iconTransform.localScale * 1.5f, 0.5f)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause();
        }

        public void AddResource()
        {
            _currentResourceAmount++;
            resourceAmountText.text = _currentResourceAmount.ToString();
            _iconTween.TogglePause();
        }
    }
}
