using System;
using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public static class Configuration
    {
        public const float RoadwayLength = 10.0f;
        public const float RoadwayWidth = 3.0f;
        public const float RoadwayHeight = 2.0f;
        public const int RoadwayNumberOfLanes = 3;
        public const float RoadwayMinSlope = -20;
        public const float RoadwayMaxSlope = 20;
        public const float RoadwayChangeSlopeAfter = 20;

        public const float WallsDistance = RoadwayWidth * RoadwayNumberOfLanes;
        public const float WallLength = 0.05f;
        public const float WallMinHeight = 8.0f;
        public const float WallMaxHeight = 12.0f;

        public const float VegetationDistance = RoadwayLength / 2.0f;

        public const bool DeterministigPCG = false;
        public const int DeterministicSeedNumber = 5;

        public static float Speed = 0.2f;
        public static Dictionary<GameObjectType, uint> GenerateAheadLimits;
        public static List<Color> WallColors = new List<Color> {Color.white, Color.gray, Color.black};
        public static int RandomSeedNumber = !DeterministigPCG ? (int) DateTime.Now.Ticks : 5;

        static Configuration()
        {
            GenerateAheadLimits = new Dictionary<GameObjectType, uint>();
            GenerateAheadLimits.Add(GameObjectType.Floor, 20);
            GenerateAheadLimits.Add(GameObjectType.Vegetation,
                (uint) (RoadwayLength * GenerateAheadLimits[GameObjectType.Floor] / VegetationDistance));
            GenerateAheadLimits.Add(GameObjectType.Wall,
                (uint) (RoadwayLength * GenerateAheadLimits[GameObjectType.Floor] / WallLength));
        }
    }
}