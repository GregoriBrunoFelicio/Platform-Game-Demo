using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class RainFireRespawn : MonoBehaviour
    {
        public GameObject Fire;

        private void Start() => StartCoroutine(CreateRainOfFire());

        private IEnumerator CreateRainOfFire()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.5f);
                Instantiate(Fire, transform.position, Quaternion.identity);
            }
        }
    }
}
