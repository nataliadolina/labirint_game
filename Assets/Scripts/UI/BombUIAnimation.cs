using Enums;
using Zenject;

namespace UI
{
    public class BombUIAnimation : PickUpUIAnimation
    {
        protected override PickUpType pickUpType { get => PickUpType.Bomb; }

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
