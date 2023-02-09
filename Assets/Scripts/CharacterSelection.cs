using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private bool vikingThor;
    private GameObject normal;
    private GameObject viking;


    private void Start()
    {
        normal = GameObject.Find("Simple Thor");
        viking = GameObject.Find("Viking Thor HD");

        normal.SetActive(true);
        viking.SetActive(false);
    }

    public void ChangeThor()
    {
        if (vikingThor)
        {
            normal.SetActive(true);
            viking.SetActive(false);
            vikingThor = false;
        }
        else
        {
            normal.SetActive(false);
            viking.SetActive(true);
            vikingThor = true;
        }
    }
}