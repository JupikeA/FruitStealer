using System.Linq;
using UnityEngine;

namespace FruitStealer
{
    public sealed class Wall : ObjectBase
    {
        private Transform leftWall;
        private Transform rightWall;

        static Wall()
        {
            prefabs[GameObjectType.Wall] = Resources.LoadAll("01 Prefabs/02 Walls").ToList();
        }

        public Wall(Vector3 position, Transform parent, float angle)
            : base(position, typeof(Wall).Name)
        {
            leftWall = GetRandomPrefab().transform;
            leftWall.position = position + new Vector3(0.0f, 0.0f, -Configuration.WallsDistance / 2.1f);
            leftWall.parent = gameObject.transform;
            leftWall.Rotate(Vector3.forward, angle, Space.World);
            Debug.LogWarning(leftWall.GetComponent<MeshRenderer>().bounds.size);
            rightWall = GetRandomPrefab().transform;
            rightWall.position = position + new Vector3(0.0f, 0.0f, Configuration.WallsDistance / 2.1f);
            rightWall.parent = gameObject.transform;
            rightWall.Rotate(Vector3.forward, angle, Space.World);

            gameObject.transform.parent = parent;
        }

        public Wall(Vector3 position)
            : base(position, typeof(Wall).Name)
        {
            leftWall = Object.Instantiate(Resources.Load("01 Prefabs/02 Walls/Wall01") as GameObject).transform
                .Find("Ground");
            leftWall.GetComponent<Renderer>().material.color = new Color(MathHelper.Range(0.0f, 1.0f),
                MathHelper.Range(0.0f, 1.0f), MathHelper.Range(0.0f, 1.0f));
            leftWall.position = position - new Vector3(0, 0, Configuration.WallsDistance / 2);
            leftWall.localScale = new Vector3(Configuration.WallLength,
                MathHelper.Range(Configuration.WallMinHeight, Configuration.WallMaxHeight), 0.2f);
            //this.leftWall.Rotate(Vector3.up, MathHelper.Range(-45.0f, 45.0f));
            //this.leftWall.GetComponent<Collider>().enabled = false;
            leftWall.parent = gameObject.transform;

            rightWall = Object.Instantiate(Resources.Load("01 Prefabs/02 Walls/Wall01") as GameObject).transform
                .Find("Ground");
            rightWall.GetComponent<Renderer>().material.color = new Color(MathHelper.Range(0.0f, 1.0f),
                MathHelper.Range(0.0f, 1.0f), MathHelper.Range(0.0f, 1.0f));
            rightWall.position = position + new Vector3(0, 0, Configuration.WallsDistance / 2);
            rightWall.localScale = new Vector3(Configuration.WallLength,
                MathHelper.Range(Configuration.WallMinHeight, Configuration.WallMaxHeight), 0.2f);
            //this.rightWall.Rotate(Vector3.up, MathHelper.Range(-45.0f, 45.0f));
            //this.rightWall.GetComponent<Collider>().enabled = false;
            rightWall.parent = gameObject.transform;
        }

        public override void Destroy()
        {
            base.Destroy();
            Object.Destroy(leftWall.gameObject);
            Object.Destroy(rightWall.gameObject);
            leftWall = null;
            rightWall = null;
        }

        public override void Move(Vector3 offset)
        {
            base.Move(offset);
            //this.leftWall.Rotate(Vector3.left, MathHelper.Range(0.0f, 5.0f));
            //this.rightWall.Rotate(Vector3.right, MathHelper.Range(-5.0f, 0.0f));
        }

        #region Properties

        public override GameObjectType Type
        {
            get { return GameObjectType.Wall; }
        }

        public override Vector3 Position
        {
            get { return leftWall.position; }
        }

        #endregion
    }
}