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
        public Slider manaBar;

        private Rigidbody2D rgdBody2D;
        private bool isJumping;
        private bool faceInRight;
        private Animator animator;

        private void Awake()
        {
            speed = 5f;
            jumpForce = 300f;
            InitializeRigidBody();
            animator = transform.GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
            Jumb();
            Atack();
            CastFirePower();
        }

        private void Move()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(horizontalInput * speed * Time.deltaTime, 0f, 0f);
            FlipFace(horizontalInput);
            WalkAnimation(horizontalInput != 0);
        }

        private void WalkAnimation(bool atack) =>

            animator.SetBool("Walk", atack);

        private void Atack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Atack_Sword");
            }
        }

        private void CastFirePower()
        {
            if (Input.GetButtonDown("Fire2") && manaBar.value >= 30)
            {
                animator.SetTrigger("Cast_Fire_Power");
                gameObject.GetComponentInChildren<FirePowerRespawn>().Fire();
                ConsumeMana();
            }
        }

        private void ConsumeMana() => manaBar.value -= 15;

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


        private void AddMana() =>
            manaBar.value += 30;

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
            if (component.gameObject.CompareTag("ManaPotion")) AddMana();
        }

    }
}
