using UnityEngine;

namespace UI
{
    internal class UIRectangleZone : MonoBehaviour
    {
        [Header("Bounds transforms")]
        [SerializeField]
        private RectTransform left;
        [SerializeField]
        private RectTransform right;
        [SerializeField]
        private RectTransform down;
        [SerializeField]
        private RectTransform up;

        internal bool IsPositionInsideZone(Vector2 position)
        {
            float minPositionX = left.position.x;
            float maxPositionX = right.position.x;
            float minPositionY = down.position.y;
            float maxPositionY = up.position.y;

            return position.x > minPositionX && position.x < maxPositionX && position.y > minPositionY && position.y < maxPositionY;
        }
    }
}