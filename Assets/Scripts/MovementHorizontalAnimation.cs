using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementHorizontalAnimation: MonoBehaviour {

    public float incrementMovement;
    public bool isInMotion = false;
    public float leftMostPosition;
    public float rightMostPosition;
    public float distanceBetweenTiles;
    private float positionGoal;

    protected void ChooseDirection(HDirections direction)
    {
        Vector3 position = gameObject.transform.position;
        if (direction == HDirections.Left)
        {
            positionGoal = position.x - distanceBetweenTiles;
            InvokeRepeating("MoveTowardsLeft", 0f, 0.05f);
        }
        else if (direction == HDirections.Right)
        {
            positionGoal = position.x + distanceBetweenTiles;
            InvokeRepeating("MoveTowardsRight", 0f, 0.05f);
        }
        else
        {
            throw new UnityException("Invalid direction for movement");
        }
    }

    private void MoveTowardsLeft()
    {
        Vector3 position = gameObject.transform.position;
        if (position.x > positionGoal)
        {
            isInMotion = true;
            gameObject.transform.position = new Vector3(position.x -= incrementMovement, position.y, position.z);
        }
        else
        {
            isInMotion = false;
            CancelInvoke();
        }
    }

    private void MoveTowardsRight()
    {
        Vector3 position = gameObject.transform.position;
        if (position.x < positionGoal)
        {
            isInMotion = true;
            gameObject.transform.position = new Vector3(position.x += incrementMovement, position.y, position.z);
        }
        else
        {
            isInMotion = false;
            CancelInvoke();
        }
    }
}

public enum HDirections {
    Left,
    Right
}
