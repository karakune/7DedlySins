using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach(string name in Input.GetJoystickNames()) {
			Debug.Log("input name: " + name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
