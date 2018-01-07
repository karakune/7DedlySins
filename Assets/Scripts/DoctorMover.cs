using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorMover : MonoBehaviour {

	public Camera cam;
	public string playerNumber = "1";

	//Input Names
	//LeftStick to move
	protected string xMove;
	protected string zMove;
	//RightStick to control camera
	protected string xRotate;
	protected string yRotate;
	//Buttons
	protected string A;
	protected string B;
	protected string X;
	protected string Y;


	//Movement speed
	public float speed;
	public float rotSpeed;
	//Jump speed
	public float jumpVelocity;
	//Define if the player can move or not
	public bool canMove;

	//Pour ne pas permettre double saut
	public bool jump;

	//Rotation horizontale
	private float xRotation;
	//Rotation Verticale
	private float yRotation;

	//Rigidbody du doctor
	protected Rigidbody rb;
	// Use this for initialization
	protected virtual void Start () {
		//Init des rotation a 0
		xRotation = 0;
		yRotation = 0;

		//Initi Input
		xMove = "LeftStickHorizontal" + playerNumber ;
		zMove = "LeftStickVertical"+ playerNumber;
		xRotate = "RightStickHorizontal"+ playerNumber;
		yRotate = "RightStickVertical"+ playerNumber;
		A = "AButton"+ playerNumber;
		B = "BButton"+ playerNumber;
		X = "XButton"+ playerNumber;
		Y = "YButton"+ playerNumber;

		//Init jump;
		jump = true;
		//The player can move
		canMove = true;

		//Get rigidBody
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	protected virtual void FixedUpdate () {
		if (canMove) {
			//Move
			transform.Translate (Input.GetAxis(xMove)*speed,0,Input.GetAxis(zMove)*speed);
			//Incrementer/Decrementer la rotation 
			xRotation += Input.GetAxis (xRotate);
			yRotation -= Input.GetAxis (yRotate);
			//Rotate Body
			transform.eulerAngles = new Vector3 (0, xRotation * rotSpeed, 0);
			//Rotate Camera
			cam.transform.eulerAngles = new Vector3 (yRotation * rotSpeed ,xRotation * rotSpeed, 0);

			//Jump with A button
			if (Input.GetButtonDown (A) && jump) 
			{
				jump = false;
				rb.velocity = Vector3.up * jumpVelocity;
			}
		}
		
	}	

	//Repermettre le joueur de sauter quand il touche le sol
	protected virtual void OnCollisionEnter(Collision other){
		// print (other.gameObject.tag);
		if (other.gameObject.tag == "Ground") {
			jump = true;
		}
	}


}
