using Enums;
using Zenject;

namespace UI
{
    public class CoinUIAnimation : PickUpUIAnimation
    {
        protected override PickUpType pickUpType { get => PickUpType.Coin; }

        public class Pool : MemoryPool<PickUpUIAnimation>
        {
            protected override void OnCreated(PickUpUIAnimation item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void OnDespawned(PickUpUIAnimation item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void OnSpawned(PickUpUIAnimation item)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}