using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JourneyToUtgard
{
    public class PlayerAttack : MonoBehaviour
    {
        private float timeBtwAttack;        // Attack speed.
        public float startTimeBtwAttack;    // Attack speed timer.

        public Transform attackPos;         // Determines the position which to attack.
        public LayerMask whatIsEnemies;     // Determines what is considered an enemy.
        public float attackRange;           // Range of the attack.
        public int damage;                  // Damage of the attack.
        public Animator animator;
        void Update()
        {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {    
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
                }
                animator.SetBool("IsAttacking", true);

            }
            timeBtwAttack = startTimeBtwAttack;
        } 
        else 
        {
            timeBtwAttack -= Time.deltaTime;
            animator.SetBool("IsAttacking", false);
        }
        }

        void onDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
};