using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public abstract class MovableBase : IMovable
    {
        protected GameObject gameObject;

        protected MovableBase(Vector3 offset, string tag)
        {
            this.gameObject = new GameObject(tag);
            this.gameObject.transform.position = offset;
            this.gameObject.tag = "CanDestroy";

        }

        public virtual void Destroy()
        {
            Object.Destroy(this.gameObject);
            this.gameObject = null;
        }

        #region Properties

        public abstract GameObjectType Type { get; }

        public abstract Vector3 Position { get; }

        #endregion

        public virtual void Move(Vector3 offset)
        {
            this.gameObject.transform.Translate(offset);
        }
    }
}