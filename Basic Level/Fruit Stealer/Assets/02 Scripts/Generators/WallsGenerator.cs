using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public class WallsGenerator : MovableGenerator
    {
        public override Queue<MovableBase> Initialize()
        {
            var queue = new Queue<MovableBase>();
            position = new Vector3(-Configuration.WallLength / 2, 0, 0);
            for (var i = 0; i <= Configuration.GenerateAheadLimits[GameObjectType.Wall]; i++)
                queue.Enqueue(GenerateNext());

            return queue;
        }

        public override MovableBase GenerateNext()
        {
            position += new Vector3(Configuration.WallLength, 0, 0);
            return new Wall(position);
        }
    }
}