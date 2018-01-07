using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingBox : MonoBehaviour {

	public List<Collider> collisionsToIgnore;

	// Use this for initialization
	void Start () {
		foreach(Collider c in collisionsToIgnore) {
			Physics.IgnoreCollision(c.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// void OnTriggerEnter (Collider other) {
	// 	if (other.tag == "Movable") {
	// 		Debug.Log("now trigger!");
	// 		gameObject.GetComponent<Collider>().isTrigger = true;
	// 	}
	// }

	// void OnTriggerExit (Collider other) {
	// 	if (other.tag == "Movable") {
	// 		Debug.Log("Now solid!");
	// 		gameObject.GetComponent<Collider>().isTrigger = false;
	// 	}
	// }
}
