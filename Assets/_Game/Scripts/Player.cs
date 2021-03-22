using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Game.Scripts
{
    public class Player : MonoBehaviour
    {
        public float speed;
        public float jumpForce;
        public Slider lifeBar;

        private Rigidbody2D rgdBody2D;
        private bool isJumping;
        private bool faceInRight;

        private void Awake()
        {
            speed = 5f;
            jumpForce = 300f;
            InitializeRigidBody();
        }

        private void Update()
        {
            Move();
            Jumb();
        }

        private void Move()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(horizontalInput * speed * Time.deltaTime, 0f, 0f);
            FlipFace(horizontalInput);
        }

        private void FlipFace(float horizontal)
        {
            if ((horizontal < 0 && !faceInRight) || (horizontal > 0 && faceInRight))
            {
                faceInRight = !faceInRight;
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        private void Jumb()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                rgdBody2D.AddForce(transform.up * jumpForce);
            }
        }

        private void InitializeRigidBody()
        {
            rgdBody2D = transform.GetComponent<Rigidbody2D>();
            rgdBody2D.freezeRotation = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }

        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit")) ResetGame();
            if (component.gameObject.CompareTag("Fire")) TakeDamage();
        }

        private void TakeDamage()
        {
            if (lifeBar.value > 0)
            {
                lifeBar.value -= 20;
                if (lifeBar.value <= 0) ResetGame();
            }
            else
            {
                ResetGame();
            }
        }

        private static void ResetGame() =>
                SceneManager.LoadScene("SampleScene");
    }
}
