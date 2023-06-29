using UnityEngine;
using Zenject;

namespace UI
{
    internal abstract class JoystickControllerBase : MonoBehaviour
    {
        [Header("Joystick")]
        [SerializeField] private GameObject joystickGameObject;
        [SerializeField] private Transform joystickTransform;
        [SerializeField] private Transform knobTransform;

        [Space]
        [SerializeField] private Transform center;
        [SerializeField] private Transform edgePoint;

        [Space]
        [SerializeField] private UIRectangleZone joystickRectangleZone;

        private Vector2 _joystickStartPosition;

        private Vector2 _joystickUpdatedPosition;
        private bool _shouldUpdateDirection = false;

#region MonoBehaviour

        private void Start()
        {
            _joystickStartPosition = knobTransform.position;
            _joystickUpdatedPosition = _joystickStartPosition;
            joystickGameObject.SetActive(false);
        }

#endregion

        private protected virtual void UpdatePlayerDirection(in Vector2 direction) { }

        private Vector2 GetKnobPositionByDirection(Vector2 direction, float distanceToCenter)
        {
            return _joystickUpdatedPosition + direction.normalized * Mathf.Clamp(distanceToCenter, 0, Vector2.Distance(center.position, edgePoint.position));
        }

        public void StartUpdateJoyctickDirection()
        {
            Vector2 touchPosition = Input.mousePosition;

            if (!joystickRectangleZone.IsPositionInsideZone(touchPosition))
            {
                _shouldUpdateDirection = false;
                return;
            }

            _shouldUpdateDirection = true;
            joystickGameObject.SetActive(true);
            SetJoystickPosition(touchPosition);
        }

        private void SetJoystickPosition(Vector2 touchPosition)
        {
            joystickTransform.position = touchPosition;
            _joystickUpdatedPosition = touchPosition;
        }

        public void SetKnobPosition()
        {
            if (!_shouldUpdateDirection)
            {
                return;
            }

            Vector2 touchPosition = Input.mousePosition;
            Vector3 knobPosition = GetKnobPositionByDirection(touchPosition - _joystickUpdatedPosition, Vector3.Distance(touchPosition, _joystickUpdatedPosition));
            UpdateDirection(knobPosition);
        }

        public void ResetJoystick()
        {
            UpdatePlayerDirection(Vector2.zero);
            joystickGameObject.SetActive(false);
        }

        private void UpdateDirection(Vector3 knobPosition)
        {
            knobTransform.position = knobPosition;
            Vector2 direction = (new Vector2(knobPosition.x, knobPosition.y) - _joystickUpdatedPosition) / Vector2.Distance(center.position, edgePoint.position);
            UpdatePlayerDirection(direction);
        }
    }
}