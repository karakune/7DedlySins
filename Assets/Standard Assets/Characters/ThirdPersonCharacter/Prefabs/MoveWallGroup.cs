using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWallGroup : MonoBehaviour {

    public GameObject wallPair;
    public bool triggerMove;
    public bool triggerOther;
    private static float positionLow = -1.25f;
    private static float positionHigh = 1.25f;
    private static float incrementMovement = 0.25f;

    private void FixedUpdate()
    {
        ValidateWallMove();
    }

    private void ValidateWallMove()
    {
        if (this.triggerOther)
        {
            ChooseWallDirection();
            this.triggerOther = !this.triggerOther;
        }
        if (this.triggerMove)
       {
            ChooseWallDirection();
            this.triggerMove = !this.triggerMove;
            this.triggerOther = false;
            wallPair.GetComponent<MoveWallGroup>().triggerOther = true;
       }
    }

    private void ChooseWallDirection()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y > positionLow)
        {
            InvokeRepeating("MoveWallLow", 0f, 0.05f);
        }
        else
        {
            InvokeRepeating("MoveWallHigh", 0f, 0.05f);
        }
    }

    private void MoveWallLow()
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

    private void MoveWallHigh()
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

    void Start()
    {
        if(this.triggerMove)
        {
            Vector3 position = gameObject.transform.position;
            if(position.y > positionLow)
                gameObject.transform.position = new Vector3(position.x, positionLow, position.z);
            else
                gameObject.transform.position = new Vector3(position.x, positionHigh, position.z);
        }
    }

}
