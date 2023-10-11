using Enums;
using Zenject;

namespace UI
{
    public class CoinUIAnimation : PickUpUIAnimation
    {
        protected override PickUpType pickUpType { get => PickUpType.Bomb; }

        public class Pool : MemoryPool<PickUpUIAnimation>
        {

        }
    }
}