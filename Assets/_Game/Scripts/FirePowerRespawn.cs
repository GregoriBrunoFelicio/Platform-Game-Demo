using UnityEngine;

namespace Assets._Game.Scripts
{
    public class FirePowerRespawn : MonoBehaviour
    {
        public GameObject FirePower;
        public GameObject Player;
        public float bulletSpeed;

        private void Awake()
        {
            bulletSpeed = 3000;
        }


        public void Fire()
        {
            CreateObjects();
        }

        private void CreateObjects()
        {
            var firePower = Instantiate(FirePower, transform.position, Quaternion.identity);
            firePower.GetComponent<Rigidbody2D>().AddForce(new Vector2(Player.transform.localScale.x, 0) * bulletSpeed);
        }
    }
}
