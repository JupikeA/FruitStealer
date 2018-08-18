using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public class ObjectsManager : MonoBehaviour
    {
        private Dictionary<GameObjectType, MovableGenerator> generators;
        private Dictionary<GameObjectType, Queue<MovableBase>> objects;

        public Transform PlayerObject;

        private void Start()
        {
            generators = new Dictionary<GameObjectType, MovableGenerator>();
            generators.Add(GameObjectType.Floor, new TerrainGenerator());

            objects = new Dictionary<GameObjectType, Queue<MovableBase>>();
            foreach (var item in generators.Keys)
                objects.Add(item, generators[item].Initialize());
        }

        private void FixedUpdate()
        {
            foreach (var type in objects.Keys)
            {
                var firstObject = objects[type].Peek();
                if (firstObject.Position.x < PlayerObject.position.x - 10)
                {
                    objects[type].Dequeue();
                    objects[type].Enqueue(generators[type].GenerateNext());
                    firstObject.Destroy();
                }
            }
        }
    }
}