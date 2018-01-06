using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWallGroup : MovementVerticalAnimation, IPossessable
{

    public GameObject wallPair;
    public bool triggerMove;
    public bool triggerOther;

    void Start()
    {
        positionHigh = 1.25f;
        positionLow = -1.25f;
        incrementMovement = 0.25f;

        if (this.triggerMove)
        {
            Vector3 position = gameObject.transform.position;
            if (position.y > positionLow)
                gameObject.transform.position = new Vector3(position.x, positionLow, position.z);
            else
                gameObject.transform.position = new Vector3(position.x, positionHigh, position.z);
        }
    }

    private void FixedUpdate()
    {
        ValidateWallMove();
    }

    private void ValidateWallMove()
    {
        if (this.triggerOther)
        {
            ChooseDirection();
            this.triggerOther = !this.triggerOther;
        }
        if (this.triggerMove)
       {
            ChooseDirection();
            this.triggerMove = !this.triggerMove;
            this.triggerOther = false;
            wallPair.GetComponent<MoveWallGroup>().triggerOther = true;
       }
    }

    public void Possess()
    {
        if (isInMotion == false) {
            this.triggerMove = !this.triggerMove;
        }
    }

    public void Glow(Color color)
    {
        Color colorWithOpacity = new Color(color.r, color.g, color.b, 0.5f);
        GameObject glowObject = gameObject.transform.Find("Glow").gameObject;
        glowObject.GetComponent<Renderer>().material.color = colorWithOpacity;
        glowObject.SetActive(true);
    }

    public void UnGlow() {
        gameObject.transform.Find("Glow").gameObject.SetActive(false);
    }
}
