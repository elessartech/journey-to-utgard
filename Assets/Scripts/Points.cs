using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JourneyToUtgard
{
    public class Points : MonoBehaviour
    {
        public Text pointsText;
        private PlayerController player;

        private void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        private void Update()
        {
            pointsText.text = ("Divine power collected " + player.points + "/" + player.pointsTotal);
        }
    }
}
