using Props.Chests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    internal class OpenButtonInput : ButtonBase
    {
        [Inject]
        private ChestAnimator _chestAnimator;

        protected override void onClick()
        {
            Debug.Log("Clicked");
            _chestAnimator.Open();
        }
    }
}