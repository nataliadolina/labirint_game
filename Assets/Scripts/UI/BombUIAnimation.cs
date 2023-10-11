using Enums;
using Zenject;

namespace UI
{
    public class BombUIAnimation : PickUpUIAnimation
    {
        protected override PickUpType pickUpType { get => PickUpType.Bomb; }

        public class Pool : MemoryPool<PickUpUIAnimation>
        {

        }
    }
}
