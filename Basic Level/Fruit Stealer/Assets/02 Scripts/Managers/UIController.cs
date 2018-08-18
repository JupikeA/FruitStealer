using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace FruitStealer
{
    public class UIController : MonoBehaviour
    {
        public Text CountLabel;
        private PlayerManager playerManager;
        public Transform PlayerObject;
        public Text ScoreLabel;
        public Text SpeedLabel;
        private Stopwatch timer;
        public Text TimerCount;

        private void Start()
        {
            timer = Stopwatch.StartNew();
            playerManager = PlayerObject.GetComponent<PlayerManager>();
        }

        private void Update()
        {
            ScoreLabel.text = "Score: " + playerManager.Score;
            CountLabel.text = "Count: " + playerManager.Count;
            TimerCount.text = "Elpsed: " + timer.Elapsed.TotalSeconds.ToString("N2");
            SpeedLabel.text = "Speed: " + playerManager.Speed.ToString("N2");
        }
    }
}