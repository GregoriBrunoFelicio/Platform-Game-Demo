using UnityEngine;

namespace Assets._Game.Scripts
{
    public class FireBall : MonoBehaviour
    {
        public float speed;

        private Rigidbody2D rgdBody2D;
        private Vector2 direction;

        private void Awake()
        {
            rgdBody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rgdBody2D.velocity = direction * speed;
        }

        public void SetDirection(Vector2 drct)
        {
            transform.Rotate(drct);
            direction = drct;
        }

        public void SetRotation(Transform trsm)
        {
            transform.localScale = trsm.localScale;
            transform.position = new Vector3(trsm.position.x, trsm.position.y - 1f);
        }

        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Ground")
                || component.gameObject.CompareTag("Limit")
                || component.gameObject.CompareTag("Enemy"))
                Destroy(gameObject);
        }
    }
}
