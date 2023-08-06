using UnityEngine;
using Zenject;
namespace Props.Chests
{
    internal enum ChestStates
    {
        Default,
        Openable,
    }

    internal class ChestGUI : MonoBehaviour
    {
        [SerializeField]
        private GUIStyle style;
        [SerializeField]
        private Canvas canvas;

        [Inject]
        private ChestAnimator _chestAnimator;

        private CustomTransform _chestTransform;

        private Camera _camera;

        internal ChestStates CurrentState;

        [Inject]
        private void OnConstruct(CustomTransform customChestTransform, Chest chest)
        {
            CurrentState = ChestStates.Default;
            _camera = Camera.main;
            _chestTransform = customChestTransform;
        }

        private void OnGUI()
        {
            switch (CurrentState)
            {
                case ChestStates.Openable:
                    OpenGUI();
                    break;
            }
            
        }
        private void OpenGUI()
        {
            Vector3 chestScreenPosition = _camera.WorldToScreenPoint(_chestTransform.Position);
            GUILayout.Window(0, new Rect(chestScreenPosition.x, _camera.pixelRect.height - chestScreenPosition.y, 120, 50), OpenWindowLayout, "");
        }

        private void OpenWindowLayout(int windowId)
        {
            GUILayout.BeginVertical();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Открыть"))
                {
                    _chestAnimator.Open();
                    CurrentState = ChestStates.Default;
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
        }
    }
}
