using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public class VegetationGenerator : MovableGenerator
    {
        public override Queue<MovableBase> Initialize()
        {
            var queue = new Queue<MovableBase>();
            position = new Vector3(-Configuration.VegetationDistance, 1.1f, 0);
            for (var i = 0; i <= Configuration.GenerateAheadLimits[GameObjectType.Vegetation]; i++)
                queue.Enqueue(GenerateNext());

            return queue;
        }

        public override MovableBase GenerateNext()
        {
            position += new Vector3(Configuration.VegetationDistance, 0, 0);
            return new Vegetation(position, null);
        }
    }
}