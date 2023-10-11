using UI;
using Enums;
using ModestTree;

namespace Spawners
{
    public class PickUpUIAnimationSpawner
    {
        private BombUIAnimation.Pool _bombUIAnimationPool;
        private CoinUIAnimation.Pool _coinUIAnimationPool;

        public PickUpUIAnimationSpawner(
            BombUIAnimation.Pool bombUIAnimationPool,
            CoinUIAnimation.Pool coinUIAnimationPool
            )
        {
            _bombUIAnimationPool = bombUIAnimationPool;
            _coinUIAnimationPool = coinUIAnimationPool;
        }

        public PickUpUIAnimation Spawn(PickUpType pickUpType)
        {
            switch (pickUpType)
            {
                case (PickUpType.Bomb):
                    return _bombUIAnimationPool.Spawn();
                case (PickUpType.Coin):
                    return _coinUIAnimationPool.Spawn();
            }
            throw Assert.CreateException();
        }

        public void Despawn(PickUpType pickUpType, PickUpUIAnimation animation)
        {
            switch (pickUpType)
            {
                case (PickUpType.Bomb):
                    _bombUIAnimationPool.Despawn(animation);
                    break;
                case (PickUpType.Coin):
                    _coinUIAnimationPool.Despawn(animation);
                    break;
            }
        }
    }
}