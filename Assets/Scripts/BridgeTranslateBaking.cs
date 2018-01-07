using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BridgeTranslateBaking : MonoBehaviour {

	private NavMeshObstacle obstacle;

	// Use this for initialization
	void Start () {
		obstacle = GetComponent<NavMeshObstacle> ();
			
	}
	
	// Update is called once per frame


	void OnTriggerEnter(Collider other){
		if (other.tag == "Possessable") {
			obstacle.enabled = false;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Possessable") {
			obstacle.enabled = true;
		}
	}
}
