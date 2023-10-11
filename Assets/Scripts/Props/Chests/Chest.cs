using Zenject;
using Enums;
using UnityEngine;

namespace Props.Chests
{
    public class Chest
    {
        public Chest(PickUpType pickUpType,
            Vector3 position,
            CustomTransform customTransform,
            ChestAnimator chestAnimator)
        {
            customTransform.Position = position;
            chestAnimator.SetUp(pickUpType);
        }

        public class Factory : PlaceholderFactory<PickUpType, Vector3, Chest>
        {

        }
    }
}
