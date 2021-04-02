using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public float maxHelth = 100;
        public Slider lifeBar;

        private float currentHelth;
        private Animator animator;


        private void Awake()
        {
            animator = transform.GetComponent<Animator>();
            lifeBar = transform.GetComponentInChildren<Slider>();
            currentHelth = maxHelth;
        }

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
        }

        private void AtackAnimation() =>

            animator.SetTrigger("Atack");

        private void DieAnimation(bool die) =>
            animator.SetBool("Died", die);

    }
}
