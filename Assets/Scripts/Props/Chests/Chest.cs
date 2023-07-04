using UI;
using UnityEngine;
using Zenject;

namespace Props.Chests
{
    internal class Chest : MonoBehaviour
    {
        private GameObject _openButton;

        [Inject]
        private void Construct(OpenButtonInput openButtonInput)
        {
            _openButton = openButtonInput.gameObject;
            _openButton.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _openButton.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _openButton.SetActive(false);
            }
        }

        public class Factory : PlaceholderFactory<Chest>
        {

        }
    }
}

