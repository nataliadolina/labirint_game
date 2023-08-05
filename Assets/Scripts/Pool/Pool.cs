using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace Pool
{
    internal class Pool<T> where T : MonoBehaviour
    {
        internal bool autoExpend { get; set; }
        private List<T> _pool;
        private ISpawner _spawner;

        private bool _autoExpand;

        internal Pool(ISpawner spawner, int count, bool autoExpand = true)
        {
            _spawner = spawner;
            _autoExpand = autoExpand;

            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = _spawner.Spawn();
            createdObject.gameObject.SetActive(isActiveByDefault);
            T poolComponent = createdObject.GetComponent<T>();
            _pool.Add(poolComponent);
            return poolComponent;
        }

        internal bool HasFreeElement(out T element)
        {
            foreach (var poolObject in _pool)
            {
                if (!poolObject.gameObject.activeInHierarchy)
                {
                    element = poolObject;
                    return true;
                }
            }

            element = null;
            return false;
        }

        internal T GetFreeELement()
        {
            if (HasFreeElement(out T element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (_autoExpand)
            {
                return CreateObject(true);
            }

            throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }
}
