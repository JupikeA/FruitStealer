using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FruitStealer
{
    public class HardObject : ObjectBase
    {
        static HardObject()
        {
            prefabs[GameObjectType.HardObject] = Resources.LoadAll("01 Prefabs/06 Objects").ToList();
        }

        public HardObject(Vector3 offset, float angle, Transform parent)
            : base(offset, typeof(HardObject).Name)
        {
            var hardObject = GetRandomPrefab();
            hardObject.transform.position = offset;
            hardObject.transform.parent = gameObject.transform;
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
            hardObject.transform.Rotate(Vector3.forward, angle, Space.World);
            gameObject.transform.parent = parent;

            LogObject(Type.ToString(), hardObject.name);
        }

        public override GameObjectType Type
        {
            get { return GameObjectType.HardObject; }
        }

        public override Vector3 Position
        {
            get { return gameObject.transform.position; }
        }
    }
}