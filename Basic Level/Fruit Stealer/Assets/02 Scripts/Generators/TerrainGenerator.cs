using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public sealed class TerrainGenerator : MovableGenerator
    {
        private static float angle;
        private static int generatedItems = 0;

        public override Queue<MovableBase> Initialize()
        {
            var queue = new Queue<MovableBase>();
            position = new Vector3(0, 0, 0);
            for (var i = 0; i <= Configuration.GenerateAheadLimits[GameObjectType.Floor]; i++)
                queue.Enqueue(GenerateNext());

            return queue;
        }

        public override MovableBase GenerateNext()
        {
            Floor floor = null;

            position += Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(Configuration.RoadwayLength * 0.5f,
                            -Configuration.RoadwayHeight / 2.0f, 0);
            floor = new Floor(position, angle);
            position += Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(Configuration.RoadwayLength * 0.5f,
                            Configuration.RoadwayHeight / 2.0f, 0);

            generatedItems++;

            if (generatedItems % Configuration.RoadwayChangeSlopeAfter == 0)
                angle = MathHelper.Range(Configuration.RoadwayMinSlope, Configuration.RoadwayMaxSlope);

            return floor;
        }
    }
}