﻿using UnityEngine;
using Zenject;

namespace Props.Chests
{
    internal class Chest : MonoBehaviour
    {
        private ChestGUI _chestGUI;

        internal Vector3 Position => transform.position;

        [Inject]
        private void Construct(ChestGUI chestGUI)
        {
            _chestGUI = chestGUI;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _chestGUI.CurrentState = ChestStates.Openable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _chestGUI.CurrentState = ChestStates.Default;
            }
        }

        public class Factory : PlaceholderFactory<Chest>
        {

        }
    }
}

