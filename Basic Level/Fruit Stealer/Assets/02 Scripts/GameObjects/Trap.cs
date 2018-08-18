using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FruitStealer
{
    public class Trap : ObjectBase
    {
        static Trap()
        {
            prefabs[GameObjectType.Trap] = Resources.LoadAll("01 Prefabs/05 Traps").ToList();
        }

        public Trap(Vector3 offset, float angle, Transform parent)
            : base(offset, typeof(Trap).Name)
        {
            var hardObject = GetRandomPrefab();
            hardObject.transform.position = offset;
            hardObject.transform.parent = gameObject.transform;
            var collider = this.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            hardObject.transform.Rotate(Vector3.forward, angle, Space.World);
            this.gameObject.transform.parent = parent;

            LogObject(Type.ToString(), hardObject.name);
        }

        public override GameObjectType Type
        {
            get { return GameObjectType.Trap; }
        }

        public override Vector3 Position
        {
            get { return this.gameObject.transform.position; }
        }
    }
}
