using System.Collections.Generic;
using UnityEngine;

namespace FruitStealer
{
    public sealed class PCGTree : Vegetation
    {
        private static readonly List<Color> branchColors = new List<Color>
        {
            new Color(92.0f / 255.0f, 43.0f / 255.0f, 43.0f / 255.0f),
            new Color(77.0f / 255.0f, 40.0f / 255.0f, 40.0f / 255.0f),
            new Color(58.0f / 255.0f, 28.0f / 255.0f, 28.0f / 255.0f)
        };

        private static readonly List<Color> leafColors = new List<Color>
        {
            Color.green,
            new Color(34.0f / 255.0f, 77.0f / 255.0f, 23.0f / 255.0f),
            new Color(9.0f / 255.0f, 148.0f / 255.0f, 65.0f / 255.0f),
            new Color(96.0f / 255.0f, 148.0f / 255.0f, 48.0f / 255.0f),
            Color.yellow
        };

        public PCGTree(Vector3 position, Transform parent, int generations)
            : base(position, parent)
        {
            gameObject.transform.parent = parent;
            var gameObjects = new Queue<GameObject>();
            var root = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            root.GetComponent<Renderer>().material.color = branchColors[MathHelper.Range(0, branchColors.Count - 1)];
            root.transform.position = position + Vector3.up * 1.0f;
            root.transform.localScale = new Vector3(0.4f, 1.0f, 0.4f);
            root.transform.parent = gameObject.transform;
            gameObjects.Enqueue(root);


            var currentLength = 1.0f;
            var currentWidth = 0.4f;
            for (var i = 0; i < generations; i++)
            {
                currentLength /= 1.3f;
                currentWidth /= 1.2f;
                var currentGameObjects = new Queue<GameObject>();
                while (gameObjects.Count > 0)
                {
                    var current = gameObjects.Dequeue();

                    var branches = 5;
                    for (var j = 0; j < branches; j++)
                    {
                        if (MathHelper.RandomBool(0.33))
                            continue;

                        var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

                        var color = i < generations - 1
                            ? branchColors[MathHelper.Range(0, branchColors.Count - 1)]
                            : leafColors[MathHelper.Range(0, leafColors.Count - 1)];
                        cylinder.GetComponent<Renderer>().material.color = color;
                        cylinder.transform.localScale = new Vector3(currentWidth, currentLength, currentWidth);

                        cylinder.transform.rotation = current.transform.rotation;
                        cylinder.transform.position = current.transform.position;


                        cylinder.transform.Rotate(current.transform.up,
                            MathHelper.Range(350.0f / branches, 370.0f / branches) * j, Space.World);
                        cylinder.transform.Rotate(cylinder.transform.forward, MathHelper.Range(30.0f, 45.0f),
                            Space.World);

                        cylinder.transform.position += current.transform.up * currentLength * 1.2f +
                                                       cylinder.transform.up * currentLength;


                        cylinder.transform.parent = gameObject.transform;
                        currentGameObjects.Enqueue(cylinder);

                        if (i == generations - 1)
                            cylinder.isStatic = true;
                    }

                    current.isStatic = true;
                }

                gameObjects = currentGameObjects;
            }
        }
    }
}