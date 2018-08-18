using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public sealed class Floor : MovableBase
    {
        private List<Transform> tiles;

        public Floor(Vector3 position, float angle)
            : base(position, typeof(Floor).Name)
        {
            tiles = new List<Transform>(Configuration.RoadwayNumberOfLanes);
            for (var i = 0; i < Configuration.RoadwayNumberOfLanes; i++)
            {
                var currentPosition = position + new Vector3(0.0f, 0.0f,
                                          Configuration.RoadwayWidth *
                                          ((Configuration.RoadwayNumberOfLanes - 1) / 2.0f - i));
                var type = GetFloorType(i);
                Transform transform = null;
                switch (type)
                {
                    case FloorType.HillSand:
                    case FloorType.HillGrass:
                        transform = CreateHill(currentPosition, angle, type);
                        break;
                    case FloorType.Grass:
                        transform = CreateGrass(currentPosition, angle, type);
                        break;
                    case FloorType.Sand:
                        transform = CreateSand(currentPosition, angle, type);
                        break;
                }

                tiles.Add(transform);
                transform.tag = "CanDestroy";
            }

            gameObject.tag = "CanDestroy";
        }

        private static FloorType GetFloorType(int i)
        {
            if (i != 0 && i != Configuration.RoadwayNumberOfLanes - 1 && MathHelper.RandomBool(0.3))
                return MathHelper.RandomBool(0.33) ? FloorType.HillSand : FloorType.HillGrass;

            if (i != 0 && i != Configuration.RoadwayNumberOfLanes - 1 && MathHelper.RandomBool(0.8))
                return FloorType.Sand;

            return FloorType.Grass;
        }

        private static GameObjectType GetObjectType(FloorType floorType)
        {
            switch (floorType)
            {
                case FloorType.HillGrass:
                    return MathHelper.RandomBool(0.8) ? GameObjectType.Fruit : GameObjectType.Trap;
                case FloorType.HillSand:
                    return MathHelper.RandomBool(0.75) ? GameObjectType.Fruit :
                        MathHelper.RandomBool(0.35) ? GameObjectType.Trap : GameObjectType.HardObject;
                case FloorType.Grass:
                    return MathHelper.RandomBool(0.75) ? GameObjectType.Vegetation :
                        MathHelper.RandomBool(0.25) ? GameObjectType.Fruit : GameObjectType.Trap;
                case FloorType.Sand:
                    return MathHelper.RandomBool(0.65) ? GameObjectType.Fruit : GameObjectType.Trap;
            }

            return GameObjectType.Trap;
        }

        public override void Destroy()
        {
            base.Destroy();

            foreach (var tile in tiles)
                Object.Destroy(tile.gameObject);

            tiles.Clear();
            tiles = null;
        }

        private Transform CreateSand(Vector3 position, float angle, FloorType floorType)
        {
            var lane = Object.Instantiate(Resources.Load("01 Prefabs/01 Terrain/Sand") as GameObject).transform;
            lane.position = position;
            lane.localScale = new Vector3(Configuration.RoadwayLength, Configuration.RoadwayHeight,
                Configuration.RoadwayWidth);
            var collider = lane.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
            collider.size = new Vector3(1.1f, 1.0f,
                floorType == FloorType.Grass || floorType == FloorType.Sand ? 1.25f : 1.05f);
            lane.parent = gameObject.transform;
            lane.Rotate(Vector3.forward, angle, Space.World);

            CreateObjects(position, angle, floorType);

            return lane;
        }

        private Transform CreateGrass(Vector3 position, float angle, FloorType floorType)
        {
            var lane = Object.Instantiate(Resources.Load("01 Prefabs/01 Terrain/Grass") as GameObject).transform;
            lane.position = position;
            lane.localScale = new Vector3(Configuration.RoadwayLength, Configuration.RoadwayHeight,
                Configuration.RoadwayWidth);
            var collider = lane.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
            collider.size = new Vector3(1.1f, 1.0f,
                floorType == FloorType.Grass || floorType == FloorType.Sand ? 1.25f : 1.05f);
            lane.parent = gameObject.transform;
            lane.Rotate(Vector3.forward, angle, Space.World);

            CreateObjects(position, angle, floorType);

            return lane;
        }

        private Transform CreateHill(Vector3 position, float angle, FloorType floorType)
        {
            angle = angle + 10.0f;
            position += Quaternion.AngleAxis(angle, Vector3.forward) *
                        new Vector3(0.0f, Configuration.RoadwayHeight / 2.0f, 0);
            var transform = MathHelper.RandomBool(0.75)
                ? CreateSand(position, angle, floorType)
                : CreateGrass(position, angle, floorType);

            return transform;
        }

        private void CreateObjects(Vector3 position, float angle, FloorType floorType)
        {
            if (position.x < 20.0f)
                return;

            position += Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(-Configuration.RoadwayLength / 2.0f,
                            Configuration.RoadwayHeight / 2.0f, 0.0f);

            var count = floorType == FloorType.HillGrass || floorType == FloorType.HillSand
                ? MathHelper.Range(4, 7)
                : MathHelper.Range(2, 5);
            for (var i = 0; i < count; i++)
            {
                var type = GetObjectType(floorType);
                ObjectBase.CreateObject(position, angle, type, gameObject.transform);
                position += Quaternion.AngleAxis(angle, Vector3.forward) *
                            new Vector3(Configuration.RoadwayLength / count, 0.0f, 0.0f);
            }
        }

        private enum FloorType
        {
            HillGrass = 0,
            HillSand = 1,
            Grass = 2,
            Sand = 3
        }

        #region Properties

        public override GameObjectType Type
        {
            get { return GameObjectType.Floor; }
        }

        public override Vector3 Position
        {
            get { return tiles[tiles.Count / 2].position; }
        }

        #endregion
    }
}