using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Enemy : MonoBehaviour
    {

        private Animator animator;

        private void Awake()
        {
            animator = transform.GetComponent<Animator>();
        }

        private void Update()
        {

        }

        private void Atack()
        {
            animator.SetTrigger("Atack");
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) Atack();
        }

    }
}
