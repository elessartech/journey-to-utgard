using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JourneyToUtgard
{
    public class PlayerController : MonoBehaviour
    {
        public int health;          // The amount of health the player has
        public int numOfHearts;     // The amount of health the player can have
        public Image[] hearts;      // An array for the hearts displayed on the ui.
        public Sprite fullHeart;
        public Sprite emptyHeart;

        public float speed;         // Players speed, jumpforce and moveinput.
        public float jumpForce;
        private float moveInput;

        public Transform spawnPoint;

        private Rigidbody2D rb;     // Player rigidbody
        public Animator animator;   // Animator animates the player with the animations that have been created.

        private bool isGrounded;        
        public Transform groundCheck;   // Checks if player has hit the ground.
        public float checkRadius;       // The radius of the check.
        public LayerMask whatIsGround;  // We will use this to determine what is ground.

        public bool facingRight = true;
        private int extraJumps;         // How many extra jumps the player has left.
        public int extraJumpValue;      // How many extra jumps the player has in total.

        private bool hasBelt;           // Checks if the player has the belt.

        public int points;              // Point calculation.
        public int pointsTotal;

        private MenuController gameOver;
        private MenuController winPanel;
        private PlayerAttack damage;                // How much damage the player does.
        private PlayerAttack startTimeBtwAttack;    // Players attack speed.

        public bool dead;
        public bool partyMode;
        public bool mirrorDamage;
        public bool ghostMode;        

        void Start()
        {
            extraJumps = extraJumpValue;
            rb = GetComponent<Rigidbody2D>();
            points = 0;
            gameOver = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuController>();
            winPanel = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuController>();
            damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
            startTimeBtwAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
            pointsTotal = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            if (ghostMode)
            {
                isGrounded = false;
            }

            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

        private void Update()
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

            if(health > numOfHearts)
            {
                health = numOfHearts;               // Health can not exceed the number of hearts the player can have
            }

            for(int i = 0; i < hearts.Length; i++) // For loop that checks health status.
            {
                if(i < health)
                {
                    hearts[i].sprite = fullHeart;  // Show full heart.
                }
                else
                {
                    hearts[i].sprite = emptyHeart; // Show empty heart.
                }

                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;      // Show the number of hearts that the variable has.
                }
                else
                {
                    hearts[i].enabled = false;     // Hide the rest of the hearts.
                }
            }

            if (isGrounded == true)
            {
                extraJumps = extraJumpValue;

                if (rb.velocity.y == 0)
                {
                    animator.SetBool("IsJumping", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0 || Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
                animator.SetBool("IsJumping", true);
                SoundManagerScript.PlaySound("ThorJumpSound");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true || Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (health <= 0 && !dead)
            {
                Debug.Log("You lose :(");
                dead = true;
                if(dead == true)
                {
                    animator.SetBool("IsDead", true);
                    SoundManagerScript.PlaySound("GameOverSound");
                    speed = 0;
                    jumpForce = 0;
                    gameOver.ShowGameOver(); // enabling game over ui element
                    gameOver.isGameOver = true;
                }
            }

            if (points == pointsTotal)
            {
                winPanel.isGameWon = true;
                winPanel.ShowWinPanel();
                SoundManagerScript.PlaySound("HornPickSound");
            }

            if (partyMode)
            {
                Party();
            }
        }

        private void Party()
        {
            Color change = new Color();
            float loc = Time.fixedTime - Mathf.Round(Time.fixedTime);

            change.r = Mathf.Sin(loc + Time.fixedTime % 3) * 0.75f + 0.25f;
            change.g = Mathf.Sin(loc + Time.fixedTime % 5 - 1) * 0.75f + 0.25f;
            change.b = Mathf.Sin(loc + Time.fixedTime % 7 - 2) * 0.75f + 0.25f;
            change.a = 1;

            gameObject.GetComponent<SpriteRenderer>().color = change;
        }

        private void Flip()
        {
            if (!dead)
            {
                facingRight = !facingRight;
                Vector3 Scaler = transform.localScale;
                Scaler.x *= -1;
                transform.localScale = Scaler;

            }
        }

        public void ReduceHealth(GameObject enemy)
        {
            if (mirrorDamage)
            {
                enemy.GetComponent<EnemyController>().health -= gameObject.GetComponent<PlayerAttack>().damage;
            }
            else
            {
                if (!dead)
                {
                    health -= 1;
                    SoundManagerScript.PlaySound("PlayerHurtSound");
                    Debug.Log("The character has taken damage! His health is now " + health);
                }
            }
        }

        void WeakenEnemies(GameObject enemy)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.health = controller.health / 10 * 7;
            controller.StartHurting();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Pick Up"))
            {
                Debug.Log("Power has been increased!");
                SoundManagerScript.PlaySound("DivinePowerSound");
                points += 1;
                damage.damage += 1;
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("Boot"))
            {
                Debug.Log("Boots have been picked up! Movement speed increased!");
                SoundManagerScript.PlaySound("BootPickSound");
                speed = speed + 0.5f;
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("Belt"))
            {
                Debug.Log("Belt has been picked up! Maximum health increased!");
                SoundManagerScript.PlaySound("BeltPickSound");
                numOfHearts = 15;
                health = 15;
                hasBelt = true;
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("Horn"))
            {
                SoundManagerScript.PlaySound("HornPickSound");
                //startTimeBtwAttack.startTimeBtwAttack = 0.1f;
                //
                foreach (Transform child in GameObject.Find("Enemies").transform)
                {
                    foreach(Transform grandchild in child.transform)
                    {
                        WeakenEnemies(grandchild.gameObject);
                    }
                }

                Debug.Log("Horn has been picked up!");

                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("Heart"))
            {
                SoundManagerScript.PlaySound("HealthPickSound");
                if (hasBelt == true)
                {
                    Debug.Log("Health restored to 15!");
                    health = 15;
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Health restored to 10!");
                    health = 10;
                    Destroy(collision.gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Spike") && !dead)
            {
                SoundManagerScript.PlaySound("SpikeHitSound");
                health -= 1;
                Debug.Log("The character has taken damage! His health is now " + health);
            }
            
        }
    }
}
 