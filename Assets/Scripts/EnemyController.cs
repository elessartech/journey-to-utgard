using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JourneyToUtgard
{
    public class EnemyController : MonoBehaviour
    {
        public int health;
        public float speed;
        public float distance;

        private bool movingRight;

        public Transform textureDetection;
        public Animator animator;

        private float deathTime;
        private float deathAnimation;
        private bool dead;

        private float attackAnimation;
        private float attackTime;
        private bool isAttacking;

        private float hurtAnimation;
        private float hurtTime;
        private bool isHurting;

        private GameObject player;
        private PlayerController playerController;
        public float originalSpeed;

        void Awake() // before any Start() functions, set up few variables
        {
            BoxCollider2D bc;
            bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc.size = new Vector2(0.2f, 0.2f);
            bc.isTrigger = true;

            originalSpeed = speed;

            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();

            deathAnimation = 2.8f; // length of death animation
            deathTime = 0;         // death timer

            attackAnimation = 1.1f; // length of attack animation
            attackTime = 0;         // attack timer

            hurtAnimation = 0.5f; // length of hurt animation
            hurtTime = 0;         // hurt timer

            animator.SetFloat("Speed", Mathf.Abs(speed));
        }

        void Update()
        {
            if (attackTime > attackAnimation) // if attack animation should stop
            {
                StopAttacking(); // stop attacking
            }

            if (hurtTime > hurtAnimation) // if hurt animation should stop
            {
                StopHurting(); // stop being hurt :(
            }

            if (health <= 0) // if enemy is dead
            {
                if (!dead) // if this is the first time its death is detected
                {
                    SoundManagerScript.PlaySound("EnemyDeathSound");
                    Die(); // set up enemy for dying
                    Debug.Log("Enemy is dying");
                }

                deathTime += Time.deltaTime; // track for how long the enemy has been dead

                if (deathTime > deathAnimation) // if it's been dead for longer than death animation
                {
                    Destroy(gameObject); // destroy enemy gameObject
                    Debug.Log("Enemy died");
                }

            }
            else // if enemy is alive
            {
                if (isAttacking) // if it's attacking
                {
                    animator.SetBool("CollidePlayer", true); // start/keep attack animation going
                    attackTime += Time.deltaTime;            // add time to attack timer
                    if (isHurting) // if it's also hurting
                    {
                        hurtTime += Time.deltaTime; // add time to hurt timer
                    }
                }
                else if (isHurting) // if it's hurt
                {
                    animator.SetBool("IsHurt", true); // start/keep hurt animation going
                    hurtTime += Time.deltaTime;       // add time to hurt timer

                }
            }

            transform.Translate(Vector2.right * speed * Time.deltaTime); // move forward

            RaycastHit2D groundInfo = Physics2D.Raycast(textureDetection.position, Vector2.down, distance); // texture detection

            if (groundInfo.collider == false || groundInfo.collider.CompareTag("Walls")) // if there's no texture ahead or the texture is a wall
            {
                TurnAround(); // turn the enemy around
            }
        }

        /* ****************************** FIGHT RELATED METHODS ****************************** */

        private void StartAttacking()
        {
            isAttacking = true;
        }

        public void StartHurting()
        {
            isHurting = true;
        }

        private void StopAttacking()
        {
            animator.SetBool("CollidePlayer", false); // stop attack animation
            isAttacking = false;                      // stop attacking
            attackTime = 0f;                          // reset attack timer
        }

        private void StopHurting()
        {
            animator.SetBool("IsHurt", false); // stop hurt animation
            isHurting = false;                 // stop being hurt :(
            hurtTime = 0f;                     // reset hurt timer
            speed = originalSpeed;             // start moving again
        }

        private void Die()
        {
            StopAttacking();                  // stop attack animation
            StopHurting();                    // stop hurt animation
            animator.SetBool("IsDead", true); // start death animation
            speed = 0f;                       // stop moving
            dead = true;                      // set that death has been detected
        }

        public void TakeDamage(int damage) // method for receiving damage
        {
            if (!dead) // if the enemy isn't dead
            {
                health -= damage; // reduce health by amount of damage
                StartHurting();   // start hurt animation
                SoundManagerScript.PlaySound("HitSound");
                Debug.Log("An enemy took " + damage + " damage, its health is now " + health);

                if (!isAttacking) // if enemy wasn't already fighting
                {
                    if ((player.transform.position.x > gameObject.transform.position.x && !movingRight) || // if player hits from back (right) or
                        (player.transform.position.x < gameObject.transform.position.x && movingRight))    // if player hits from back (left)
                    {
                        StopHurting();    // stop being hurt :(
                        TurnAround();     // turn around towards the backstabbing player
                        StartAttacking(); // attack him
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D col) // if something collides the enemy
        {
            if (!dead) // if the enemy isn't dead
            {
                if (col.name == "Player") // if the collider is Player
                {
                    StartAttacking();                // start attacking
                    playerController.ReduceHealth(gameObject); // reduce player's health by calling ReduceHealth method
                }
            }
        }

        /* ****************************** MOVEMENT RELATED METHODS ****************************** */

        private void TurnAround() // method for turning around
        {
            if (movingRight == true) // if the enemy is moving right
            {
                transform.eulerAngles = new Vector3(0, -180, 0); // turn around
                movingRight = false;                             // go left
            }
            else // if the enemy is moving left
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // turn around
                movingRight = true;                           // go right
            }
        }
    }
}