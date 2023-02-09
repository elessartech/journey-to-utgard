using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JourneyToUtgard
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform cam;
        private float camX;
        private float camY;

        private float camDX;
        private float camDY;

        private float scale;

        //private float acceleration; // how fast can the camera accelerate
        public float speed; // speed of camera

        private float maxSpeed; // fastest the camera can go if player is within maxDistance
        private float maxDistance; // maximum distance to player

        private GameObject player; // access the Player, which is parent
        private PlayerController playerController;

        private float playerX;
        private float playerY;

        private float distX; // distance X to player
        private float distY; // distance Y to player
        private float dist;  // distance to player

        private float time;

        void Start()
        {
            cam = gameObject.GetComponent<Transform>();
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();

            //acceleration = 8f;
            speed = 0f;

            playerX = player.transform.position.x;
            playerY = player.transform.position.y;

            camX = playerX;
            camY = playerY;

            scale = 1000f;
        }

        void Update()
        {
            time = Time.deltaTime;

            playerX = player.transform.position.x;
            playerY = player.transform.position.y;

            distX = camX - playerX;
            distY = camY - playerY;
            dist = Mathf.Sqrt(Mathf.Pow(distX, 2f) + Mathf.Pow(distY, 2f));

            camDX = cam.position.x - camX;
            camDY = cam.position.y - camY;

            speed = Mathf.Sqrt(Mathf.Pow(camDX, 2f) + Mathf.Pow(camDY, 2f)) * time * scale;

            if (dist > 10f)
            {
                //cam.position = new Vector3(distX, distY, -10f);
            }

            /*
            player_dy = player_vy * time + 0.5f * player_ay * timeSquared;
            player_dx = player_vx * time + 0.5f * player_ax * timeSquared;

            player_vy += player_ay * time;
            player_vx += player_ax * time;

            player.transform.position += new Vector3(player_dx, player_dy, 0);
            */

            camX = cam.position.x;
            camY = cam.position.y;

            cam.position = new Vector3(playerX, playerY, -10f);
        }
    }
}
