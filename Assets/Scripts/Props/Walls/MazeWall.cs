using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Props.Walls
{
    internal class MazeWall : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<MazeWall>
        {

        }
    }
}
