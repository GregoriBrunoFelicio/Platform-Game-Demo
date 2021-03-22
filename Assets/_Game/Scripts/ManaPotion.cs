using UnityEngine;

namespace Assets._Game.Scripts
{
    public class ManaPotion : MonoBehaviour
    {
        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Player")) Destroy(gameObject);
        }

    }
}
