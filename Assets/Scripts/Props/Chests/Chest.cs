using UnityEngine;
using Zenject;

namespace Props.Chests
{
    internal class Chest : MonoBehaviour
    {
        private ChestGUI _chestGUI;

        [Inject]
        private void Construct(ChestGUI chestGUI)
        {
            _chestGUI = chestGUI;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider other = collision.collider;
            if (other.CompareTag("Player"))
            {
                _chestGUI.CurrentState = ChestStates.Openable;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Collider other = collision.collider;
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

