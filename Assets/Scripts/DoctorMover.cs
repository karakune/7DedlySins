using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorMover : MonoBehaviour {

	public Camera cam;

	//Horizontal1 pour Doctor et Horizontal2 pour jester
	public string x = "LeftStickHorizontal1";

	//Vertical1 pour Doctor et Vertical2 pour Jester
	public string z = "LeftStickVertical1";

	//Movement speed
	public float speed;
	//Jump speed
	public float jumpVelocity;

	//Pour ne pas permettre double saut
	private bool jump;

	//Rotation horizontale
	private float xRotation;
	//Rotation Verticale
	private float yRotation;

	//Rigidbody du doctor
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		//Init des rotation a 0
		xRotation = 0;
		yRotation = 0;
		jump = true;
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Move
		transform.Translate (Input.GetAxis(x)*speed,0,Input.GetAxis(z)*speed);
		//Incrementer/Decrementer la rotation 
		xRotation += Input.GetAxis ("RightStickHorizontal1");
		yRotation -= Input.GetAxis ("RightStickVertical1");
		//Rotate Body
		transform.eulerAngles = new Vector3 (0, xRotation, 0);
		//Rotate Camera
		cam.transform.eulerAngles = new Vector3 (yRotation,xRotation , 0);

		//Jump with A button
		if (Input.GetButtonDown ("AButton1") && jump) 
		{
			rb.velocity = Vector3.up * jumpVelocity;
		}
		
	}	

	//Repermettre le joueur de sauter quand il touche le sol
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "ground" && jump == false) {
			jump = true;
		}
	}

	//Ne plus permettre le joueur de sauter quand il est dans les airs
	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "ground" && jump == true) {
			jump = false;
		}
	}


	void Update(){
		
	}
}
