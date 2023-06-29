using UnityEngine;
using Zenject;

namespace Props.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        public class Settings
        {
            public float Speed;
        }

        public class Factory : PlaceholderFactory<Enemy>
        {

        }
    }
}
