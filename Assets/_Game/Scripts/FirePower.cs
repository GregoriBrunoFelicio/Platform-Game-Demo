using UnityEngine;

namespace Assets._Game.Scripts
{
    public class FirePower : MonoBehaviour
    {
        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit")) Destroy(gameObject);
            if (component.gameObject.CompareTag("Fire")) Destroy(gameObject);
            if (component.gameObject.CompareTag("Ground")) Destroy(gameObject);
        }
    }
}
