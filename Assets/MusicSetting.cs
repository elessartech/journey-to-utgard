using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetting : MonoBehaviour
{
    void Start()
    {
        if (!GameObject.Find("Messenger").GetComponent<PassInfo>().music)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
