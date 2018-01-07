using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingRangeController : MonoBehaviour {




	void OnTriggerEnter(Collider other){
		//If jester is near doctor
		if (other.gameObject.tag == "Jester"){
			DoctorController.jesterVisible = true;
		}


	}

	void OnTriggerExit(Collider other){
		//If jester is far from doctor
		if (other.gameObject.tag == "Jester"){
			DoctorController.jesterVisible = false;
		}
	}
}
