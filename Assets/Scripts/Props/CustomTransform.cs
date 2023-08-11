using System;
using UnityEngine;
using Zenject;

namespace Props
{
    internal class CustomTransform : MonoBehaviour
    {
        [SerializeField]
        private Settings settings;

        private Transform _positionTransform;
        private Transform _localScaleTransform;
        private Rigidbody _rigidbody;
        private GameObject _gameObject;

        internal Vector3 Position
        {
            get => _positionTransform.position;
            set => _positionTransform.position = value;
        }

        internal Vector3 LocalScale
        {
            get => _localScaleTransform.localScale;
            set => _localScaleTransform.localScale = value;
        }

        internal GameObject GameObject
        {
            get => _gameObject;
        }

        internal Rigidbody Rigidbody
        {
            get => _rigidbody;
        }

        [Inject]
        private void OnConstruct()
        {
            _positionTransform = settings.PositionTransform;
            _localScaleTransform = settings.LocalScaleTransform;
            _gameObject = _positionTransform.gameObject;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            public Transform PositionTransform;
            [SerializeField]
            public Transform LocalScaleTransform;
        }
    }
}
