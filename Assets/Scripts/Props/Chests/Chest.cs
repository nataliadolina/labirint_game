using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Props.Chests
{
    public class Chest : ChestBase
    {
        public class Factory : PlaceholderFactory<Chest>
        {

        }
    }
}

