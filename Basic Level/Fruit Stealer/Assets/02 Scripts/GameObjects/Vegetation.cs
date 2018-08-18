using System.Linq;
using UnityEngine;

namespace FruitStealer
{
    public class Vegetation : ObjectBase
    {
        static Vegetation()
        {
            prefabs[GameObjectType.Vegetation] = Resources.LoadAll("01 Prefabs/03 Vegetation").ToList();
        }


        public Vegetation(Vector3 position, Transform parent)
            : base(position, typeof(Vegetation).Name)
        {
            var vegetation = GetRandomPrefab();
            vegetation.transform.parent = gameObject.transform;
            vegetation.transform.position = position;

            var collider1 = vegetation.gameObject.AddComponent<BoxCollider>();
            collider1.isTrigger = false;

            gameObject.transform.parent = parent;

            LogObject(Type.ToString(), vegetation.name);
        }

        public override GameObjectType Type
        {
            get { return GameObjectType.Vegetation; }
        }

        public override Vector3 Position
        {
            get { return gameObject.transform.position; }
        }

        public override void Destroy()
        {
            base.Destroy();
            Object.Destroy(gameObject);
            gameObject = null;
        }
    }
}