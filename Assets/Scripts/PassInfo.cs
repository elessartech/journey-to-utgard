using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassInfo : MonoBehaviour
{
    public bool viking;
    public string player;
    public bool music;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = "Player";
        music = true;
    }

    public void MuteMusic()
    {
        music = false;
    }

    public void PlayMusic()
    {
        music = true;
    }
}
