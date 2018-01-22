using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class JesterController : JesterMover{

	public GameObject monster;
	public GameObject doctor;
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

	public Image healthFillClockImage;
	public Image spellFillClockImage;

	public int frameCounter = 0;

	public override void Start () {
		
		base.Start ();

		// maxHealth = 100;
		// health = 100;
		// maxDistance = 0;
		// stunRange = 5;
		// stunCd = 5;
		// stunDuration = 5;
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
			possessable.gameObject.GetComponent<IPossessable>().Glow(Color.blue);
		}

		if (selectedPossessable != null) {
			selectedPossessable.gameObject.GetComponent<IPossessable>().Glow(Color.yellow);
		}

		if (Input.GetButtonDown(X)) {
			if (selectedPossessable != null) {
				selectedPossessable.gameObject.GetComponent<IPossessable>().Possess();
			}
		}

		if (Input.GetButtonDown(Lb)) {
			//cycle forwards through possessables
			UpdateSelectedPossessable(Indexes.Next);
		}

		if (Input.GetButtonDown(Rb)) {
			//cycle backwards through possessables
			UpdateSelectedPossessable(Indexes.Previous);
		}

		// //Invisibility detector
		// if (Input.GetButtonDown(Y)) {
		// 	gameObject.transform.Find("JesterInvisibilityDetector").gameObject.SetActive(true);
		// }
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
		if (canMove) {
			if (canStun && Input.GetButtonDown (B)) {
				Debug.Log("I stun!");
				StunSkill ();
			}
		}
		if (!canStun) {
			frameCounter++;
			if (frameCounter == 60) {
				frameCounter = 0;
				UpdateCooldown();
			}
		}


	}

	void HealingAura(){
		//Check if doctor is near jester
		RaycastHit hit;
		healthDecreasing = true;
		Debug.DrawRay(transform.position,(doctor.transform.position - transform.position), Color.green);
		if (Physics.Raycast(transform.position,(doctor.transform.position - transform.position), out hit, maxDistance)){	
			if (hit.collider.tag == "Doctor" || hit.collider.tag == "HealRange") {
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

	public void UpdateUIHealth()
	{
		healthFillClockImage.fillAmount = health / maxHealth;
	}

	public void UpdateCooldown()
	{
		spellFillClockImage.fillAmount += 0.25f;
	}


	void DecreaseHealth(){
		//decrease 1 health every one second
		if (timePassed > 1 && health>0) {
			health--;
			UpdateUIHealth ();
			timePassed = 0;
		}
		timePassed += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Movable") || other.CompareTag("Possessable")) {
			foreach (Collider possessable in possessables) {
				if (other == possessable) {
					return;
				}
			}
			possessables.Add(other);
			UpdateSelectedPossessable(Indexes.Last);
		}
	}	

	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Movable") || other.CompareTag("Possessable")) {
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
		if(Physics.Raycast(transform.position,(monster.transform.position - transform.position), out hit, stunRange) ){				
			if (hit.collider.tag == "Monster") {
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
		monster.GetComponent<NavMeshAgent> ().SetDestination (monster.transform.position);
		yield return  new WaitForSeconds (stunDuration);
		monster.GetComponent<NavMeshAgent> ().SetDestination (doctor.transform.position);
	}

	//The jester can't stun for the cooldown duration
	IEnumerator StunCD(){
		spellFillClockImage.fillAmount = 0;
		yield return new WaitForSeconds (stunCd);
		canStun = true;
	}


	void Die()
    {
        Debug.Log("You died!");
        canMove = false;	
		CheckpointManager.gameOver = true;
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
