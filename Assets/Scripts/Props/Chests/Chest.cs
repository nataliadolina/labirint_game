using Zenject;
using Enums;
using UnityEngine;

namespace Props.Chests
{
    public class Chest : MonoBehaviour
    {
        private CustomTransform _customTransform;
        private ChestAnimator _chestAnimator;

        [Inject]
        private void Construct(
            CustomTransform customTransform,
            ChestAnimator chestAnimator)
        {
            _customTransform = customTransform;
            _chestAnimator = chestAnimator;
        }

        public void Init(Vector3 position, PickUpType pickUpType)
        {
            _customTransform.Position = position;
            _chestAnimator.SetUp(pickUpType);
        }

        public class Factory : PlaceholderFactory<Chest>
        {

        }
    }
}
