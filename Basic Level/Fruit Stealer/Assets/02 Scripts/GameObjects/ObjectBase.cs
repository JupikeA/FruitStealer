using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public abstract class ObjectBase : MovableBase
    {
        protected static Dictionary<GameObjectType, List<Object>> prefabs;
        private static Dictionary<string, Dictionary<string, int>> counts;

        static ObjectBase()
        {
            prefabs = new Dictionary<GameObjectType, List<Object>>();
            counts = new Dictionary<string, Dictionary<string, int>>(); 
        }

        protected ObjectBase(Vector3 offset, string tag)
            : base(offset, tag)
        {
        }

        public abstract override GameObjectType Type { get; }

        public override Vector3 Position
        {
            get { return gameObject.transform.position; }
        }

        public static void CreateObject(Vector3 position, float angle, GameObjectType type, Transform parent)
        {
            switch (type)
            {
                case GameObjectType.Fruit:
                    new Fruit(position, parent);
                    break;
                case GameObjectType.Vegetation:
                    new Vegetation(position, parent);
                    break;
                case GameObjectType.HardObject:
                    new HardObject(position, angle, parent);
                    break;
                case GameObjectType.Trap:
                    new Trap(position, angle, parent);
                    break;
            }
        }

        protected GameObject GetRandomPrefab()
        {
            var prefabsOfType = prefabs[Type];
            return Object.Instantiate(prefabsOfType[MathHelper.Range(0, prefabsOfType.Count)]) as GameObject;
        }

        public static Dictionary<string, Dictionary<string, int>> Counts
        {
            get { return counts; }
        }

        public static void LogObject(string type, string objectName)
        {
            Dictionary<string, int> values;
            if (!counts.TryGetValue(type, out values))
                counts.Add(type, values = new Dictionary<string, int>());

            int count;
            if (!values.TryGetValue(objectName, out count))
                values.Add(objectName, 0);
            ++values[objectName];
        }
    }
}