using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public float maxHelth = 100;
        public Transform playerPosition;

        private float currentHelth;
        private Animator animator;
        private Slider lifeBar;
        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            animator = transform.GetComponent<Animator>();
            lifeBar = transform.GetComponentInChildren<Slider>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            currentHelth = maxHelth;
        }

        private void Update()
        {
            FlipFace();
        }

        private void FlipFace() => spriteRenderer.flipX = playerPosition.transform.position.x < transform.position.x;

        public void TakeDamage(float damage)
        {
            AtackAnimation();
            currentHelth -= damage;
            lifeBar.value = currentHelth;

            if (currentHelth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            DieAnimation(true);
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
            Destroy(gameObject, 3f);
        }


        private void AtackAnimation() =>

            animator.SetTrigger("Atack");

        private void DieAnimation(bool die) =>
            animator.SetBool("Died", die);

    }
}
