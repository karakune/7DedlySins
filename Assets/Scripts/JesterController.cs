using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterController : MonoBehaviour {

	public Camera cam;
	// private GameObject character;
	public bool canPossess = false;
	public List<Collider> possessables;
	public Collider selectedPossessable;

	// Use this for initialization
	void Start () {
		// character = gameObject;
		possessables = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (possessables.Count > 0) {
			canPossess = true;
		} else {
			canPossess = false;
		}

		foreach(Collider possessable in possessables) {
			possessable.gameObject.GetComponent<MoveWallGroup>().Glow(Color.blue);
		}

		if (selectedPossessable != null) {
			selectedPossessable.gameObject.GetComponent<MoveWallGroup>().Glow(Color.yellow);
		}

		if (Input.GetButtonDown("XButton2")) {
			if (selectedPossessable != null) {
				selectedPossessable.gameObject.GetComponent<MoveWallGroup>().Possess();
			}
		}

		if (Input.GetButtonDown("LB2")) {
			//cycle forwards through possessables
			UpdateSelectedPossessable(Indexes.Next);
		}

		if (Input.GetButtonDown("RB2")) {
			//cycle backwards through possessables
			UpdateSelectedPossessable(Indexes.Previous);
		}

		if (Input.GetButtonDown("YButton2")) {
			gameObject.transform.Find("JesterInvisibilityDetector").gameObject.SetActive(true);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Movable")) {
			possessables.Add(other);
			UpdateSelectedPossessable(Indexes.Last);
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Movable")) {
			other.gameObject.GetComponent<IPossessable>().UnGlow();
			possessables.Remove(other);
			if (other == selectedPossessable) {
				UpdateSelectedPossessable(Indexes.First);
			}
		}
	}

	void UpdateSelectedPossessable (Indexes index) {
		if (possessables.Count == 0) {
			selectedPossessable = null;
		} else {
			switch (index) {
				case Indexes.First:
					selectedPossessable = possessables[0];
					break;
				case Indexes.Last:
					selectedPossessable = possessables[possessables.Count - 1];
					break;
				case Indexes.Previous:
					int currentlySelectedIndex = possessables.FindIndex(x => selectedPossessable == x);
					if (currentlySelectedIndex > 0) {
						selectedPossessable = possessables[currentlySelectedIndex - 1];
					} else {
						selectedPossessable = possessables[possessables.Count - 1];
					}
					break;
				case Indexes.Next:
					currentlySelectedIndex = possessables.FindIndex(x => selectedPossessable == x);
					if (currentlySelectedIndex < possessables.Count - 1) {
						selectedPossessable = possessables[currentlySelectedIndex + 1];
					} else {
						selectedPossessable = possessables[0];
					}
					break;
				default:
					break;		
			}
		}

	}
}

enum Indexes {
	Previous,
	Next,
	First,
	Last
}



		// Vector3 fwd = character.transform.TransformDirection(Vector3.forward);
		// Ray movableWallDetector = new Ray(character.transform.position, fwd);
		// RaycastHit hit;
		// if (Physics.Raycast(movableWallDetector, out hit)) 
		// 	Debug.Log("Raycast hitted to: " + hit.collider);{
		// 	// if (hit.collider.CompareTag("Movable")) {
		// 	// 	//make glow
		// 	// 	if (Input.GetButtonDown("XButton1")) {
		// 	// 		//move wall
		// 	// 	}
		// 	// }
		// }