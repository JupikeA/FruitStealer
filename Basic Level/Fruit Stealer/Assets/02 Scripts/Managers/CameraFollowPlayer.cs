using UnityEngine;

namespace FruitStealer
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        private Vector3 offset;
        public GameObject PlayerObject;

        private void Start()
        {
            offset = transform.position - PlayerObject.transform.position;
        }

        private void LateUpdate()
        {
            transform.position = PlayerObject.transform.position + offset;
        }
    }
}