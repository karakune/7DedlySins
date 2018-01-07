using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MovementRotationAnimation : MonoBehaviour {

    public float incrementAngle;
    public bool isInMotion = false;
	public float angleToRotate;
	float rotationGoal;

	public void Rotate(RotationDirections direction) {
        Vector3 rotation = gameObject.transform.rotation.eulerAngles;
		if (direction == RotationDirections.Clockwise) {
			rotationGoal = rotation.y + angleToRotate;
			Debug.Log("Y rotation: " + rotation.y);
			Debug.Log("Rotation goal: " + rotationGoal);
			InvokeRepeating("RotateClockWise", 0f, 0.05f);
		}
	}

    private void RotateClockWise()
    {
        Vector3 rotation = gameObject.transform.rotation.eulerAngles;
		rotation.y = (float)Math.Round(rotation.y);
        if (rotation.y < rotationGoal && rotationGoal < 359.9f)
        {
            isInMotion = true;
            gameObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y += incrementAngle, rotation.z);
        }
		else if (rotationGoal >= 360f && rotation.y > (rotationGoal % 360f)) {
            isInMotion = true;
            gameObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y += incrementAngle, rotation.z);
		}
        else
        {
			Debug.Log("cancelling invoke");
            isInMotion = false;
            CancelInvoke();
        }
    }
}

public enum RotationDirections {
	Clockwise,
	CounterClockWise
}
