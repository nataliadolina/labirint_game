using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    internal abstract class ButtonBase : MonoBehaviour
    {
        private Button _button;

        [Inject]
        private void Construct()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(onClick);
            Debug.Log(_button.onClick);
        }

        protected abstract void onClick();
    }
}
