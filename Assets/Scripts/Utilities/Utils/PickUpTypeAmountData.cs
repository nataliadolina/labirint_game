using System;
using Enums;

namespace Utilities.Utils
{
    [Serializable]
    public struct PickUpTypeAmountData
    {
        public PickUpType PickUpType;
        public int PickUpAmount;

        public PickUpTypeAmountData(PickUpType  pickUpType, int pickUpAmount)
        {
            PickUpType = pickUpType;
            PickUpAmount = pickUpAmount;
        }
    }
}
