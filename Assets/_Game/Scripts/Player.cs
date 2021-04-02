using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets._Game.Scripts
{
    public class Player : MonoBehaviour
    {
        public float speed;
        public float jumpForce;
        public Slider lifeBar;
        public Slider manaBar;
        public GameObject fireBall;
        public Transform atackPoint;
        public LayerMask enemyLayers;

        private float horizontalInput;
        private bool isJumping;
        private bool faceInRight;
        private bool isDefending;
        private Rigidbody2D rgdBody2D;
        private Animator animator;


        private void Awake()
        {
            speed = 5f;
            jumpForce = 400f;
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
            LaunchFireBall();
        }

        private void Move()
        {
            if (!isDefending)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(horizontalInput * speed * Time.deltaTime, 0f, 0f);
                FlipFace(horizontalInput);
                WalkAnimation(horizontalInput != 0);
            }
        }

        private void Jumb()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isDefending)
            {
                isJumping = true;
                rgdBody2D.AddForce(Vector3.up * jumpForce);
                JumpAnimation(true);
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
                var hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, 0.5f, enemyLayers);

                foreach (var enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(20);
                }
            }
        }


        private void Defend()
        {
            if (Input.GetButton("Fire2") && !isJumping)
            {

                DefenseAnimation(true);
                isDefending = true;
            }
            else
            {
                DefenseAnimation(false);
                isDefending = false;
            }
        }

        private void LaunchFireBall()
        {
            if (Input.GetKeyDown(KeyCode.E) && !isDefending && manaBar.value >= 20)
            {
                Vector3 direction = Math.Abs(transform.localScale.x - 1f) < 1f ? Vector2.right : Vector2.left;
                var fire = Instantiate(fireBall, transform.position + direction * 1f, Quaternion.identity);
                fire.GetComponent<FireBall>().SetRotation(transform);
                fire.GetComponent<FireBall>().SetDirection(direction);
                CastFireBallAnimation();
                SpendMana(10);
            }
        }

        private void TakeDamage(int damage)
        {
            TakeDamageAnimation();
            lifeBar.value -= damage;
            if (lifeBar.value <= 0) ResetGame();
        }

        private static void ResetGame() =>
             SceneManager.LoadScene("SampleScene");

        private void AtackAnimation() => animator.SetTrigger("Atack_Sword");

        private void TakeDamageAnimation() => animator.SetTrigger("Take_Damage");

        private void WalkAnimation(bool atack) =>

            animator.SetBool("Walk", atack);

        private void DefenseAnimation(bool defense) =>

                  animator.SetBool("Defense", defense);

        private void CastFireBallAnimation() => animator.SetTrigger("Cast_Fire_Ball");

        private void JumpAnimation(bool jumping) => animator.SetBool("Jump", jumping);

        private void SpendMana(int amount) => manaBar.value -= amount;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
                JumpAnimation(false);
            }
        }


        private void OnTriggerEnter2D(Component component)
        {
            if (component.gameObject.CompareTag("Limit")) ResetGame();
            if (component.gameObject.CompareTag("Fire")) TakeDamage(20);
            if (component.gameObject.CompareTag("Enemy")) TakeDamage(1);
        }
    }
}
