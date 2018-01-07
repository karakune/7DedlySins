using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterMover : MonoBehaviour {

	public Camera cam;
	public string playerNumber = "2";


	//Input Names
	//LeftStick to move
	protected string xMove;
	protected string zMove;
	//RightStick to control camera
	protected string xRotate;
	//Buttons
	protected string A;
	protected string B;
	protected string X;
	protected string Y;
	protected string Rb;
	protected string Lb;

	//Movement speed
	public float speed;
	//Define if the player can move or not
	public bool canMove;

	//angle de la camera
	public float cameraAngle = 60;

	//Rotation horizontale
	private float xRotation;

	// Use this for initialization
	protected virtual void Start () {
		//Init rotation
		xRotation = 0;
		//Initi Input
		xMove = "LeftStickHorizontal" + playerNumber ;
		zMove = "LeftStickVertical"+ playerNumber;
		xRotate = "RightStickHorizontal"+ playerNumber;
		A = "AButton"+ playerNumber;
		B = "BButton"+ playerNumber;
		X = "XButton"+ playerNumber;
		Y = "YButton"+ playerNumber;
		Rb = "RB"+ playerNumber;
		Lb = "LB"+ playerNumber;
		//The player can move
		canMove = true;

	}

	// Update is called once per frame
	protected virtual void FixedUpdate () {
		if (canMove) {
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
}
