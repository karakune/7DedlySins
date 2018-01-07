using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTriggerWall : MovementVerticalAnimation {

	public GameObject affectedWall;
	public bool isForever;
	private bool isTriggered = false;
	public static bool firstActivated = false;
	public static bool secondActivated = false;

	void Update(){
		if (this.name == "Switch_Trigger_Exit (3)" &&  firstActivated && secondActivated && !isTriggered) {
			InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
			affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
			isTriggered = true;
		}

		if (this.name == "Switch_Trigger_Exit (3)" && isTriggered && ( !firstActivated || !secondActivated ))
		{
			InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
			affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
			isTriggered = false;
		}

	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Doctor" || other.tag == "Monster" || other.tag == "MovableObject") {
			if (this.name == "Switch_Trigger_Exit (3)") {
				firstActivated = true;
			} else if (this.name == "Switch_Trigger_Exit (4)") {
				secondActivated = true;
			}
		}


	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Doctor" || other.tag == "Monster" || other.tag == "MovableObject") {
			if (this.name == "Switch_Trigger_Exit (3)") {
				firstActivated = false;
			} else if (this.name == "Switch_Trigger_Exit (4)") {
				secondActivated = false;
			}
		}
	}

	private void OnTriggerStay(Collider other){
		if (other.tag == "Doctor" || other.tag == "Monster" || other.tag == "MovableObject") {
			if (this.name == "Switch_Trigger_Exit (3)") {
				firstActivated = true;
			} else if (this.name == "Switch_Trigger_Exit (4)") {
				secondActivated = true;
			}
		}
	}
}
