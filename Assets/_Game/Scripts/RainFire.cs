using UnityEngine;

namespace Assets._Game.Scripts
{
    public class RainFire : MonoBehaviour
    {
        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit"))
            {
                Destroy(gameObject);
            }
        }

    }
}
