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
	//healing skill cooldown
	public float healingCd;

	//JesterController script
	private JesterController jesterController;
	//True if jester is seen by the doctor
	private bool jesterVisible;
	//True => the doctor can use his healing skill. After that, the skill is deactivated for the cooldown duration (healingCd)
	private bool canHeal;


	protected override void Start () {
		//Call of parent.Start()
		base.Start ();
		//Get JesterController script
		jesterController = jester.GetComponent<JesterController> ();
		//At start jester not visible
		jesterVisible = false;
		//At start Doctor can use his healing skill
		canHeal = true;
		//healing range is equal to the radius of the sphere collider attached to the doctor. 0.8 is good enough
		GetComponent<SphereCollider>().radius = healingRange;
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
		if (canMove){
			Heal ();
		}

	}

	void Heal(){
		//If jester is seen => heal
		if (canHeal && Input.GetButtonDown (B) && jesterVisible) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, (jester.transform.position - transform.position), out hit, healingRange)) {
				jesterController.health = jesterController.maxHealth;
				canHeal = false;
				StartCoroutine (HealingCD ());
			}
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

	//The docotor can heal again after the healingCd
	IEnumerator HealingCD(){
		yield return new WaitForSeconds (healingCd);
		canHeal = true;
	}



	void Die(){
		canMove = false;
	
	}
}
