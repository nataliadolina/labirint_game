using UnityEngine;


namespace Maze
{
    internal class FieldLimits : MonoBehaviour
    {
        [SerializeField]
        private float xLimitOffset;
        [SerializeField]
        private float zLimitOffset;

        private float _minX;
        private float _maxX;
        private float _minZ;
        private float _maxZ;

        private void Start()
        {
            _minX = GetUpLeft().x;
            _maxX = GetUpRight().x;
            _minZ = GetDownLeft().z;
            _maxZ = GetUpLeft().z;
        }

        internal Vector3 ClampPlayerPosition(Vector3 playerPosition)
        {
            return new Vector3(Mathf.Clamp(playerPosition.x, _minX, _maxX), playerPosition.y, Mathf.Clamp(playerPosition.z, _minZ, _maxZ));
        }

        private Vector3 GetUpLeft()
        {
            Vector3 position = transform.position;
            float x = position.x - xLimitOffset;
            float y = position.y;
            float z = position.z + zLimitOffset;
            return new Vector3(x, y, z);
        }

        private Vector3 GetDownLeft()
        {
            Vector3 position = transform.position;
            float x = position.x - xLimitOffset;
            float y = position.y;
            float z = position.z - zLimitOffset;
            return new Vector3(x, y, z);
        }

        private Vector3 GetUpRight()
        {
            Vector3 position = transform.position;
            float x = position.x + xLimitOffset;
            float y = position.y;
            float z = position.z + zLimitOffset;
            return new Vector3(x, y, z);
        }

        private Vector3 GetDownRight()
        {
            Vector3 position = transform.position;
            float x = position.x + xLimitOffset;
            float y = position.y;
            float z = position.z - zLimitOffset;
            return new Vector3(x, y, z);
        }

#if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GetUpLeft(), GetDownLeft());
            Gizmos.DrawLine(GetUpRight(), GetDownRight());
            Gizmos.DrawLine(GetUpLeft(), GetUpRight());
            Gizmos.DrawLine(GetDownLeft(), GetDownRight());
        }

#endif
    }
}
