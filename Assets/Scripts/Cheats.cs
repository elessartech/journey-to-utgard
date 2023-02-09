using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JourneyToUtgard
{

    public class Cheats : MonoBehaviour
    {
        public string input;
        private PlayerController player;

        void Start()
        {
            player = gameObject.GetComponent<PlayerController>();
        }

        void Update()
        {
            DetectPressedKeyOrButton();
            if (input.Length > 7)
            {
                input = input.Substring(input.Length - 7, 7);
            }

            switch (input)
            {
                case "LETSFLY":
                    player.extraJumpValue = 10000;
                    input = "";
                    break;
                case "GETWELL":
                    player.health = player.numOfHearts;
                    input = "";
                    break;
                case "LETSRUN":
                    player.speed = 20;
                    input = "";
                    break;
                case "GODLIKE":
                    gameObject.GetComponent<PlayerAttack>().damage = 999999;
                    input = "";
                    break;
                case "GOODBYE":
                    player.health = 0;
                    input = "";
                    break;
                case "PARTYON":
                    player.partyMode = true;
                    input = "";
                    break;
                case "NOTOUCH":
                    player.mirrorDamage = true;
                    input = "";
                    break;
                case "GETSOME":
                    player.points += 10;
                    if(player.points > player.pointsTotal)
                    {
                        player.points = player.pointsTotal;
                    }
                    input = "";
                    break;
                case "GHOSTLY": // needs some work
                    player.ghostMode = true;
                    //Destroy(gameObject.GetComponent<Rigidbody2D>());
                    input = "";
                    break;
                case "GRAVITY":
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
                    input = "";
                    break;
                case "PETRIFY":
                    player.speed = 0f;
                    input = "";
                    break;
                case "SMALLER":
                    Transform tr = gameObject.GetComponent<Transform>();
                    tr.localScale = new Vector3(tr.localScale.x*0.5f, tr.localScale.y * 0.5f, tr.localScale.z * 0.5f);
                    input = "";
                    break;
                case "GREATER":
                    Transform trg = gameObject.GetComponent<Transform>();
                    trg.localScale = new Vector3(trg.localScale.x * 2f, trg.localScale.y * 2f, trg.localScale.z * 2f);
                    input = "";
                    break;
                case "CHEATER":
                    player.extraJumpValue = 10000;
                    player.health = player.numOfHearts;
                    player.speed = 20;
                    gameObject.GetComponent<PlayerAttack>().damage = 999999;
                    player.partyMode = true;
                    player.mirrorDamage = true;
                    input = "";
                    break;
                default:
                    break;
            }
        }

        //
        //

        public void DetectPressedKeyOrButton()
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                    input += kcode;
            }
        }
    }
}
