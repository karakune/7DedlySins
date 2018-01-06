using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterMover : MonoBehaviour {

	//Horizontal1 pour Doctor et Horizontal2 pour jester
	public string x = "LeftStickHorizontal2";

	//Vertical1 pour Doctor et Vertical2 pour Jester
	public string z = "LeftStickVertical2";

	//Movement speed
	public float speed;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Move
		transform.Translate (Input.GetAxis(x)*speed,0,Input.GetAxis(z)*speed);
		
	}
}
