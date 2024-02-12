using UnityEngine;
using Props.Chests;
using Enums;

namespace Spawners
{
    internal class ChestSpawner
    {
        private Chest.Factory _chestFactory;

        internal ChestSpawner(Chest.Factory chestFactory)
        {
            _chestFactory = chestFactory;
        }

        internal void Spawn(Vector3 position, PickUpType pickUpType)
        {
            Chest chest = _chestFactory.Create();
            chest.Init(position, pickUpType);
        }
    }
}
