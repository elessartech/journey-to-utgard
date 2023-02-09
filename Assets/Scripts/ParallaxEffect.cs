using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float width;
    private float height;
    private float posX;
    private float posY;
    private float startposY;
    public GameObject cam;
    public float parallaxEffect;
    public bool vertical;
    public bool underground;

    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;

        width = GetComponent<SpriteRenderer>().bounds.size.x;  // background width
        height = GetComponent<SpriteRenderer>().bounds.size.y; // background height
    }


    void FixedUpdate()
    {
        Vector3 pos = cam.transform.position; // variable for camera position

        float dist = (pos.x * parallaxEffect);       // camera position x scaled with parallax value
        float temp = (pos.x * (1 - parallaxEffect)); // camera position x scaled with parallax value reversed

        if (temp + 10 > posX + width) // if (reverse scaled) camera has moved further rigth than width
        {                        // then:
            posX += width;       // - move the background right by width
        }
        if (temp - 10 < posX - width) // if (reverse scaled) camera has moved further left than width
        {                        // then:
            posX -= width;       // - move the background left by width
        }

        if (vertical)                                          // if the background is set to be vertical
        {                                                      // then:
            if (underground)                                   // - if the background is set to be underground
            {                                                  // - then:
                if (pos.y - 10 < posY - height)                // - - if camera has moved further down than height
                {                                              // - - then:
                    posY -= height;                            // - - - move the background down by height
                }
                if (pos.y + 10 > posY + height && posY < 0)    // - - if camera has moved further up than height and is under ground level
                {                                              // - - then:
                    posY += height;                            // - - - move the background up by height
                }
                if(pos.y > 0)
                {
                    posY -= height;
                }
            }
            else                                               // - if the background is not underground (so it is sky)
            {                                                  // - then:
                if (pos.y - 10 < posY - height && posY > 0)    // - - if camera has moved further down than height and is over ground level
                {                                              // - - then:
                    posY -= height;                            // - - - move the background down by height
                }
                if (pos.y + 10 > posY + height)                // - - if camera has moved further up than height
                {                                              // - - then:
                    posY += height;                            // - - - move the background up by height
                }
                if(pos.y < 10)
                {
                    posY += height;
                }
            }
            transform.position = new Vector3(posX + dist, posY, transform.position.z); // apply move vector
        }
        else // otherwise the background isn't set to be vertical and the Y-position is kept stable
        {
            transform.position = new Vector3(posX + dist, 8f, transform.position.z); // apply move vector
        }
    }
}
