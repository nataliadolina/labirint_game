using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IResourceCounter
    {
        public PickUpType PickUpType { get; }
        public Vector3 IconPosition { get; }

        public void AddResource();
    }
}
