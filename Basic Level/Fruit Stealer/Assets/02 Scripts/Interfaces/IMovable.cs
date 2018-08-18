using UnityEngine;

namespace FruitStealer
{
    internal interface IMovable
    {
        GameObjectType Type { get; }

        Vector3 Position { get; }

        void Destroy();

        void Move(Vector3 offset);
    }
}