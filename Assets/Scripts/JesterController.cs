using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterController : JesterMover{

	public GameObject Doctor;
	//Actual health and max health
	public float health;
	public float maxHealth;
	//Max distance from doctor before health start decreasing
	public float maxDistance;
	//Range of the stun skill
	public float stunRange;
	//stun duration
	public float stunDuration;
	//If true health start decreasing
	public bool healthDecreasing;

	//Time counter (return to zero after every 1 second)
	private float timePassed;


	protected override void Start () {
		base.Start ();
		//Health does not decrease at start
		healthDecreasing = false;
		timePassed = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		healingAura ();

		if (health == 0) {
			Die ();
		}
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
		if (canMove) {
			StunSkill ();
		}

	}

	void healingAura(){
		//Check if doctor is near jester
		RaycastHit hit;
		healthDecreasing = true;
		if (Physics.Raycast(transform.position,(Doctor.transform.position - transform.position),out hit,maxDistance)){				
			if (hit.collider.tag == "Player") {
				healthDecreasing = false;
			}
		} 
		//if jester is alive and far away from doctor, health start decreasing
		if (healthDecreasing && canMove) {
			StartCoroutine ("DecreaseHealth");
		} else {
			StopCoroutine ("DecreaseHealth");
		}
	}


	void DecreaseHealth(){
		//decrease 1 health every one second
		if (timePassed > 1 && health>0) {
			health--;
			timePassed = 0;
		}
		timePassed += Time.deltaTime;
	}

	//When doctor near jester => healthDecreasing = false;
	void OnTriggerEnter(Collider other){
		
	}	
		

	void StunSkill(){
		RaycastHit hit;
		//Change this with monster pos
		if (Input.GetButtonDown(B) && Physics.Raycast(transform.position,(Doctor.transform.position - transform.position),out hit,stunRange) ){				
			if (hit.collider.tag == "Player") {
				StartCoroutine (freeze());
			}
		} 
	}

	IEnumerator freeze(){
		Doctor.GetComponent<DoctorController> ().canMove = false;
		yield return  new WaitForSeconds (stunDuration);
		Doctor.GetComponent<DoctorController> ().canMove = true;
	}



	void Die(){
		canMove = false;	
	}




}
