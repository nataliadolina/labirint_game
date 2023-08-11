using UnityEngine;
using Zenject;
using Pool;
using Interfaces;

namespace Props.Chests
{
    internal class PickUpUIAnimationLauncher : MonoBehaviour
    {
        [Inject]
        private PickUpPool _pickUpPool;

        private Sprite _sprite;
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private IResourceCounter _resourceCounter;
        private CustomTransform _chestTransform;

        [Inject]
        private void Construct(
            PickUpPool pickUpPool,
            CustomTransform customTransform)  
        {
            _pickUpPool = pickUpPool;
            _chestTransform = customTransform;
        }

        internal void SetUp(Vector3 endPoint, IResourceCounter resourceCounter, Sprite sprite)
        {
            _startPoint = Camera.main.WorldToScreenPoint(_chestTransform.Position);
            _resourceCounter = resourceCounter;
            _endPoint = endPoint;
            _sprite = sprite;
        }

        internal void LaunchAnimation()
        {
            var animation = _pickUpPool.GetFreeElement();
            animation.SetUpAnimation(_startPoint, _endPoint, _sprite, _resourceCounter);
            animation.StartAnimation();
        }

        internal class Factory : PlaceholderFactory<PickUpUIAnimationLauncher>
        {

        }
    }
}