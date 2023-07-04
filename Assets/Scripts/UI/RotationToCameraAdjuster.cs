using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    internal class RotationToCameraAdjuster : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            _rectTransform.forward = Camera.main.transform.forward;
        }
    }
}
