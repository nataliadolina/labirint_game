using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Pool;
using UI;

namespace Spawners
{
    internal class PickUpUISpawner : ISpawner
    {
        private PickUpUIAnimation.Factory _factory;
        
        internal PickUpUISpawner(PickUpUIAnimation.Factory pickUpUIAnimationFactory)
        {
            _factory = pickUpUIAnimationFactory;
        }

        public GameObject Spawn()
        {
            PickUpUIAnimation pickUp = _factory.Create();
            return pickUp.gameObject;
        }
    }
}
