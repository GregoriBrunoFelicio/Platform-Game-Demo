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
        private Animator animator;
        private bool isDefending;

        private void Awake()
        {
            speed = 5f;
            jumpForce = 300f;
            rgdBody2D = transform.GetComponent<Rigidbody2D>();
            rgdBody2D.freezeRotation = true;
            animator = transform.GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
            Jumb();
            Atack();
            Defend();
        }

        private void Move()
        {
            if (!isDefending)
            {
                var horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(horizontalInput * speed * Time.deltaTime, 0f, 0f);
                FlipFace(horizontalInput);
                WalkAnimation(horizontalInput != 0);
            }
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


        private void Atack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                AtackAnimation();
            }
        }


        private void Defend()
        {
            var defending = Input.GetButton("Fire2");
            DefenseAnimation(defending);
            isDefending = defending;
        }

        private void Jumb()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                rgdBody2D.AddForce(transform.up * jumpForce);
            }
        }

        private void TakeDamage(int value)
        {
            if (lifeBar.value > 0)
            {
                lifeBar.value -= value;
                if (lifeBar.value <= 0) ResetGame();
            }
            else
            {
                ResetGame();
            }
            TakeDamageAnimation();
        }

        private static void ResetGame() =>
             SceneManager.LoadScene("SampleScene");

        private void AtackAnimation() => animator.SetTrigger("Atack_Sword");

        private void TakeDamageAnimation() => animator.SetTrigger("Take_Damage");

        private void WalkAnimation(bool atack) =>

            animator.SetBool("Walk", atack);

        private void DefenseAnimation(bool defense) =>

                  animator.SetBool("Defense", defense);

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground")) isJumping = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) TakeDamage(1);
        }

        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit")) ResetGame();
            if (component.gameObject.CompareTag("Fire")) TakeDamage(20);
        }
    }
}
