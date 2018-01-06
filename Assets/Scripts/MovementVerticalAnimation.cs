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

    protected void ChooseDirection()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y > positionLow)
        {
            InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
        }
        else
        {
            InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
        }
    }

    private void MoveTowardsLow()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y > positionLow)
        {
            gameObject.transform.position = new Vector3(position.x, position.y -= incrementMovement, position.z);
        }
        else
        {
            CancelInvoke();
        }
    }

    private void MoveTowardsHigh()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y < positionHigh)
        {
            gameObject.transform.position = new Vector3(position.x, position.y += incrementMovement, position.z);
        }
        else
        {
            CancelInvoke();
        }
    }
}
