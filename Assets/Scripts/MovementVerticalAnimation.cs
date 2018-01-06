using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementVerticalAnimation: MonoBehaviour {

    public float positionLow;
    public float positionHigh;
    public float incrementMovement;
    public bool isInMotion = false;

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
