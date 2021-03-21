using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Camera : MonoBehaviour
    {
        public GameObject player;
        public float offset = 1.37f;

        private void Update() => SetCameraPosition();

        private void SetCameraPosition()
        {
            var newPosition = transform.position;
            newPosition.x = player.transform.position.x;
            newPosition.x += offset;
            transform.position = newPosition;
        }
    }
}
