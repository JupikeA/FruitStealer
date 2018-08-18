using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public abstract class MovableGenerator : IGenerator<MovableBase>
    {
        protected Vector3 position;

        public abstract Queue<MovableBase> Initialize();

        public abstract MovableBase GenerateNext();
    }
}