using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe servant seulement à faire l'animation d'un mouvement vertical
 * ex : descente et montée des murs ou d'un bouton
 */
public abstract class MovementVerticalAnimation: MonoBehaviour {

    public float positionLow;
    public float positionHigh;
    public float incrementMovement;
    public bool isInMotion = false;
    public AudioSource openSound;
    public AudioSource closeSound;

    protected void ChooseDirection()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y > positionLow)
        {
            closeSound.Play();
            InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
        }
        else
        {
            openSound.Play();
            InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
        }
    }

    private void MoveTowardsLow()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y > positionLow)
        {
            isInMotion = true;
            gameObject.transform.position = new Vector3(position.x, position.y -= incrementMovement, position.z);
        }
        else
        {
            isInMotion = false;
            CancelInvoke();
        }
    }

    private void MoveTowardsHigh()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y < positionHigh)
        {
            isInMotion = true;
            gameObject.transform.position = new Vector3(position.x, position.y += incrementMovement, position.z);
        }
        else
        {
            isInMotion = false;
            CancelInvoke();
        }
    }
}
