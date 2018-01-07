using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe exécutant un bouton qui doit trigger la descente ou la montée d'un mur
 */
public class TriggerWall : MovementVerticalAnimation
{

    public GameObject affectedWall;
    public bool isForever;
    private bool isTriggered = false;
    public AudioSource downSound;
    public AudioSource upSound;

    bool playOnlyOnceDown = false;
    bool playOnlyOnceUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            if (other.gameObject.tag == "Doctor")
            {
                if (!playOnlyOnceDown)
                {
                    Debug.Log("playing downsound");
                    playOnlyOnceDown = true;
                    downSound.Play();
                }
                playOnlyOnceUp = false;
                InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
                affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
                isTriggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Doctor")
        {
            if (!isForever)
            {
                if (!playOnlyOnceUp)
                {
                    Debug.Log("playing upsound");
                    playOnlyOnceUp = true;
                    upSound.Play();
                }
                playOnlyOnceDown = false;
                InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
                affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
                isTriggered = false;
            }
        }
    }
}
