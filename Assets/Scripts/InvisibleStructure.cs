using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleStructure : MonoBehaviour {

	public Collider triggerCollider;
	public Collider physicalCollider;

	// Use this for initialization
	void Start () {
		SetVisible(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.name == "JesterInvisibilityDetector") {
			SetVisible(true);
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "JesterInvisibilityDetector") {
			SetVisible(false);
		}
	}

	void SetVisible (bool isVisible) {
		gameObject.GetComponent<MeshRenderer>().enabled = isVisible;
		physicalCollider.enabled = isVisible;
	}
}
