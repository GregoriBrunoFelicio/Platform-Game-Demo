using UnityEngine;

namespace Assets._Game.Scripts
{
    public class RainFire : MonoBehaviour
    {
        private void Start()
        {
            transform.Rotate(Vector3.forward * -90);
        }

        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit"))
            {
                Destroy(gameObject);
            }
        }
    }
}
