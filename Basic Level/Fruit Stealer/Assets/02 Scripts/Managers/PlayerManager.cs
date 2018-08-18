using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FruitStealer;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace FruitStealer
{
    public class PlayerManager : MonoBehaviour
    {
        public int Score = 0;
        public int Count = 0;
        public float Speed = 1.0f;
        private Stopwatch timer;
        private bool finished = false;

        private void Start()
        {
            timer = Stopwatch.StartNew();
        }

        private void FixedUpdate()
        {
        }


        private void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Fruit")
            {
                this.Score += other.GetComponentInChildren<FruitController>().Points;
                this.Count++;
                Destroy(other.gameObject);
            }
            
            if (other.gameObject.name == typeof(Trap).Name)
            {

                var explosion = Object
                    .Instantiate(
                        Resources.Load("01 Prefabs/Explosion") as
                            GameObject).transform;

                explosion.position = transform.position + new Vector3(2.0f, 0.0f, 0.0f);

                if (!finished)
                    using (var writer =
                        File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            "FruitStealer.txt")))
                    {
                        writer.WriteLine(this.timer.Elapsed.TotalSeconds + "\t" + this.Score + "\t" + this.Count);
                        writer.Flush();
                    }

                finished = true;
            }

            //var builder = new StringBuilder();

            //foreach (var values in ObjectBase.Counts)
            //foreach (var value in values.Value)
            //    builder.AppendLine(values.Key + "\t" + value.Key + "\t" + value.Value);

            //Debug.LogWarning(builder.ToString());
        }
    }
}