using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts
{
    public class Enemy : MonoBehaviour
    {


        public float maxHelth = 100;

        private float currentHelth;
        private Animator animator;
        public Slider lifeBar;


        private void Awake()
        {
            animator = transform.GetComponent<Animator>();
            lifeBar = transform.GetComponentInChildren<Slider>();
            currentHelth = maxHelth;
        }

        private void Update()
        {
        }

        public void TakeDamage(float damage)
        {
            animator.SetTrigger("Atack");

            currentHelth -= damage;
            lifeBar.value = currentHelth;

            if (currentHelth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            animator.SetBool("Died", true);
            GetComponent<Collider2D>().enabled = false;
            enabled = false;

        }

        //private void Atack()
        //{
        //    animator.SetTrigger("Atack");
        //}

        //private void OnCollisionStay2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Player")) Atack();
        //}

    }
}
