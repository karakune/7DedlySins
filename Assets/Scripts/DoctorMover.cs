using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorMover : MonoBehaviour {

	public Camera cam;
	public float playerNumber = 1;

	//Input Names
	//LeftStick to move
	private string xMove;
	private string zMove;
	//RightStick to control camera
	private string xRotate;
	private string yRotate;
	//Buttons
	protected string A;
	protected string B;
	protected string X;
	protected string Y;


	//Movement speed
	public float speed;
	//Jump speed
	public float jumpVelocity;
	//Define if the player can move or not
	protected bool canMove;

	//Pour ne pas permettre double saut
	private bool jump;

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
			transform.eulerAngles = new Vector3 (0, xRotation, 0);
			//Rotate Camera
			cam.transform.eulerAngles = new Vector3 (yRotation,xRotation , 0);

			//Jump with A button
			if (Input.GetButtonDown (A) && jump) 
			{
				rb.velocity = Vector3.up * jumpVelocity;
			}
		}
		
	}	

	//Repermettre le joueur de sauter quand il touche le sol
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Ground" && jump == false) {
			jump = true;
		}
	}

	//Ne plus permettre le joueur de sauter quand il est dans les airs
	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "Ground" && jump == true) {
			jump = false;
		}
	}
}
