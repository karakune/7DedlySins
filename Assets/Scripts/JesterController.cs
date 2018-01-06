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
	//stun skill cooldown
	public float stunCd;
	//If true health start decreasing
	public bool healthDecreasing;

	//Time counter (return to zero after every 1 second)
	private float timePassed;
	//True => the jester can use his stun skill. After that, the skill is deactivated for the cooldown duration (stunCd)
	public bool canStun;
	
	public bool canPossess = false;
	public List<Collider> possessables;
	public Collider selectedPossessable;


	protected override void Start () {
		base.Start ();
		//Health does not decrease at start
		healthDecreasing = false;
		timePassed = 0;
		//At start jester can use his stun skill
		canStun = true;
		possessables = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		HealingAura ();

		if (health == 0) {
			Die ();
		}

		//management of possessable items
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

		//Invisibility detector
		if (Input.GetButtonDown("YButton2")) {
			gameObject.transform.Find("JesterInvisibilityDetector").gameObject.SetActive(true);
		}
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
		if (canMove) {
			if (canStun && Input.GetButtonDown (B)) {
				StunSkill ();
			}
		}

	}

	void HealingAura(){
		//Check if doctor is near jester
		RaycastHit hit;
		healthDecreasing = true;
		if (Physics.Raycast(transform.position,(Doctor.transform.position - transform.position),out hit,maxDistance)){				
			if (hit.collider.tag == "Doctor") {
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
		
	//Check if the skill can be activated
	void StunSkill(){
		RaycastHit hit;
		//Change this with monster pos
		if(Physics.Raycast(transform.position,(Doctor.transform.position - transform.position),out hit,stunRange) ){				
			if (hit.collider.tag == "Doctor") {
				canStun = false;
				//Freeze target
				StartCoroutine (Freeze());
				//Activate stun skill cooldown
				StartCoroutine (StunCD ());
			}
		} 
	}

	//The stun target cant move for the stun duration
	IEnumerator Freeze(){
		Doctor.GetComponent<DoctorController> ().canMove = false;
		yield return  new WaitForSeconds (stunDuration);
		Doctor.GetComponent<DoctorController> ().canMove = true;
	}

	//The jester can't stun for the cooldown duration
	IEnumerator StunCD(){
		yield return new WaitForSeconds (stunCd);
		canStun = true;
	}


	void Die(){
		canMove = false;	
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
