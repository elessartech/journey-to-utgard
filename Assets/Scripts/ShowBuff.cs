using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuff : MonoBehaviour
{
    public GameObject beltText;
    public GameObject hornText;
    public GameObject bootText;

    private void Start() // Script to display text when a certain object is collided with
    {
        beltText.SetActive(false);
        hornText.SetActive(false);
        bootText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Belt"))
        {
            beltText.SetActive(true);
            StartCoroutine("WaitForSec1");
        }

        if (collision.CompareTag("Horn"))
        {
            hornText.SetActive(true);
            StartCoroutine("WaitForSec2");
        }

        if (collision.CompareTag("Boot"))
        {
            bootText.SetActive(true);
            StartCoroutine("WaitForSec3");
        }
    }
    IEnumerator WaitForSec1()
    {
        yield return new WaitForSeconds(6);
        Destroy(beltText);
    }

    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(6);
        Destroy(hornText);
    }

    IEnumerator WaitForSec3()
    {
        yield return new WaitForSeconds(6);
        Destroy(bootText);
    }
}
