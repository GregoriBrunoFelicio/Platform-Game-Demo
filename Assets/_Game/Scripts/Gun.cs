using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Gun : MonoBehaviour
    {
        public GameObject Bullet;
        public GameObject Player;

        public float bulletSpeed;

        private void Start()
        {
            bulletSpeed = 3000;
        }

        private void Update()
        {
            Fire();
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CreateObjects();
            }
        }

        private void CreateObjects()
        {
            var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Player.transform.localScale.x, 0) * bulletSpeed);
        }
    }
}
