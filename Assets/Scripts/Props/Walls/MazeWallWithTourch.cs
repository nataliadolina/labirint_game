using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Props.Walls
{
    internal class MazeWallWithTourch : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<MazeWallWithTourch>
        {

        }
    }
}
