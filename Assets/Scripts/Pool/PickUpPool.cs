using UnityEngine;
using UI;
using Spawners;

namespace Pool
{
    internal class PickUpPool
    {
        private Pool<PickUpUIAnimation> _pool;
        internal PickUpPool(PickUpUISpawner spawner)
        {
            _pool = new Pool<PickUpUIAnimation>(spawner, 0);
        }

        internal Transform GetFreeElement()
        {
            PickUpUIAnimation pickUp = _pool.GetFreeElement();
            pickUp.StartAnimation();
            return pickUp.transform;
        }
    }
}
