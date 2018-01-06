using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : DoctorMover {

	public GameObject jester;
	//Actual and max health
	public float health;
	public float maxHealth;
	//Range of healing ability
	public float healingRange;

	//JesterController script
	private JesterController jesterController;
	//True if jester is seen by the doctor
	private bool jesterVisible;


	protected override void Start () {
		//Call of parent.Start()
		base.Start ();
		//Get JesterController script
		jesterController = jester.GetComponent<JesterController> ();
		//At start jester not visible
		jesterVisible = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (health == 0) {
			Die ();
		}
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
		//Activate skill
		if (Input.GetButtonDown (B) && jesterVisible) {
			Heal ();
		}
	}

	void Heal(){
		//If jester is seen => heal
		RaycastHit hit;
		if (Physics.Raycast (transform.position, (jester.transform.position-transform.position), out hit, healingRange)) {
			jesterController.health = jesterController.maxHealth;
		}

			

	}

	void OnTriggerEnter(Collider other){
		//If jester is near doctor
		jesterVisible = true;
	}
	void OnTriggerExit(Collider other){
		//If jester is far from doctor
		jesterVisible = false;
	}




	void Die(){
		canMove = false;
	
	}
}
