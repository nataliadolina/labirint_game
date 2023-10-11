using System;
using UnityEngine;
using Zenject;

namespace Props
{
    public class CustomTransform : MonoBehaviour
    {
        [SerializeField]
        private Settings settings;

        private Transform _positionTransform;
        private Transform _localScaleTransform;
        private Rigidbody _rigidbody;
        private GameObject _gameObject;

        public Vector3 Position
        {
            get => _positionTransform.position;
            set => _positionTransform.position = value;
        }

        public Vector3 LocalScale
        {
            get => _localScaleTransform.localScale;
            set => _localScaleTransform.localScale = value;
        }

        public GameObject GameObject
        {
            get => _gameObject;
        }

        public Rigidbody Rigidbody
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
