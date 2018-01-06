using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterMover : MonoBehaviour {

	public Camera cam;
	public string playerNumber = "2";


	//Input Names
	//LeftStick to move
	private string xMove;
	private string zMove;
	//RightStick to control camera
	private string xRotate;
	//Buttons
	private string A;
	private string B;
	private string X;
	private string Y;

	//Movement speed
	public float speed;

	//angle de la camera
	public float cameraAngle = 60;

	//Rotation horizontale
	private float xRotation;

	// Use this for initialization
	void Start () {
		xRotation = 0;
		xMove = "LeftStickHorizontal" + playerNumber ;
		zMove = "LeftStickVertical"+ playerNumber;
		xRotate = "RightStickHorizontal"+ playerNumber;
		A = "AButton"+ playerNumber;
		B = "BButton"+ playerNumber;
		X = "XButton"+ playerNumber;
		Y = "YButton"+ playerNumber;

	}

	// Update is called once per frame
	void FixedUpdate () {
		//Move
		transform.Translate (Input.GetAxis(xMove)*speed,0,Input.GetAxis(zMove)*speed);

		//Incrementer/Decrementer la rotation 
		xRotation += Input.GetAxis (xRotate);
		//Rotate Body
		transform.eulerAngles = new Vector3 (0, xRotation, 0);
		//Rotate Camera
		cam.transform.eulerAngles = new Vector3 (cameraAngle,xRotation ,0);
		
	}
}
