using System;
using UnityEngine;

namespace States 
{
    internal abstract class State : IDisposable
    {
        public virtual void Update() { }

        public virtual void Start()
        {
            // optionally overridden
        }

        public virtual void Dispose()
        {
            // optionally overridden
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            // optionally overridden
        }
    }
}
