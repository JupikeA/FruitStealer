using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FruitStealer
{
    public sealed class Fruit : ObjectBase
    {
        static Fruit()
        {
            prefabs[GameObjectType.Fruit] = Resources.LoadAll("01 Prefabs/04 Fruit").ToList();
        }

        public Fruit(Vector3 offset, Transform parent)
            : base(offset, typeof(Fruit).Name)
        {
            var fruit = GetRandomPrefab();
            fruit.transform.position = offset;
            fruit.transform.parent = gameObject.transform;
            fruit.transform.localScale = new Vector3(10, 10, 10);
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            gameObject.transform.parent = parent;

            LogObject(Type.ToString(), fruit.name);
        }

        public override GameObjectType Type
        {
            get { return GameObjectType.Fruit; }
        }

        public override Vector3 Position
        {
            get { return gameObject.transform.position; }
        }
    }
}