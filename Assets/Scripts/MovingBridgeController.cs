using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridgeController : MovementHorizontalAnimation, IPossessable
{

    private bool triggerMove = false;
    private GameObject jester;

    // Use this for initialization
    void Start()
    {
        jester = GameObject.Find("Jester");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Possess()
    {
        AudioClip open = Resources.Load("SFX_Door_Open") as AudioClip;
        AudioClip close = Resources.Load("SFX_Door_close") as AudioClip;

        if (jester.transform.position.x < transform.position.x && transform.position.x < rightMostPosition)
        {
            ChooseDirection(HDirections.Right);
            if (isInMotion == false)
            {
                if (this.triggerMove)
                {
                    AudioSource.PlayClipAtPoint(open, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(close, transform.position);
                }
                this.triggerMove = !this.triggerMove;
            }
        }
        else if (jester.transform.position.x > transform.position.x && transform.position.x > leftMostPosition)
        {
            ChooseDirection(HDirections.Left);
            if (isInMotion == false)
            {
                if (this.triggerMove)
                {
                    AudioSource.PlayClipAtPoint(open, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(close, transform.position);
                }
                this.triggerMove = !this.triggerMove;
            }
        }
    }

    public void Glow(Color color)
    {
        Color colorWithOpacity = new Color(color.r, color.g, color.b, 0.5f);
        if (gameObject.transform.Find("Glow"))
        {
            GameObject glowObject = gameObject.transform.Find("Glow").gameObject;
            glowObject.GetComponent<Renderer>().material.color = colorWithOpacity;
            glowObject.SetActive(true);
        }
    }

    public void UnGlow()
    {
        if (gameObject.transform.Find("Glow"))
        {
            gameObject.transform.Find("Glow").gameObject.SetActive(false);
        }
    }
}
