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
	public static bool jesterVisible=false;
	//True => the doctor can use his healing skill. After that, the skill is deactivated for the cooldown duration (healingCd)
	public bool canHeal;
	//True => The doctor is near a movable object
	public bool canMoveObject;
	//The movable object
	private GameObject movableObject;


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
		GetComponentInChildren<SphereCollider>().radius = healingRange;
		//At start the doctor wont move an object
		canMoveObject = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (health == 0) {
			Die ();
		}
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();

		if (canMove){

			//If Doctor wants to use healing skill and jester is visible and not far away
			if (canHeal && Input.GetButtonDown (B) && jesterVisible) {
				Heal ();
			}

			//If Doctor wants to move object and is colliding with it => the object will follow doctors pos
			if (canMoveObject && Input.GetButtonDown (X)) {
				MoveObject ();
			}
			//When button X up, Detach object from doctor
			if (movableObject!=null && Input.GetButtonUp(X)){
				movableObject.transform.SetParent (null);
				movableObject.GetComponent<Rigidbody> ().isKinematic = true;
			}

		}

	}

	void Heal(){
		//If jester is seen => heal
		RaycastHit hit;
		if (Physics.Raycast (transform.position, (jester.transform.position - transform.position), out hit, healingRange*2)) {
			jesterController.health = jesterController.maxHealth;
			canHeal = false;
			//launch skill cooldown
			StartCoroutine (HealingCD ());
		}
	}

	//Move the object by setting kinematic false and by attaching movable object to doctor
	void MoveObject(){
		movableObject.GetComponent<Rigidbody> ().isKinematic = false;
		movableObject.transform.SetParent (transform);
	}



	//The docotor can heal again after the healingCd
	IEnumerator HealingCD(){
		yield return new WaitForSeconds (healingCd);
		canHeal = true;
	}

	void Die()
    {
        Debug.Log("You died!");
        canMove = false;
	}

	protected override void OnCollisionEnter(Collision other){
		base.OnCollisionEnter (other);
		//If Doctor collides with movable object, store a reference of that object
		if (other.gameObject.tag == "MovableObject") {
			canMoveObject = true;
			movableObject = other.gameObject;
		}

		if (other.gameObject.tag == "Monster") {
			Die ();
			health = 0;
		}
	}

	void OnCollisionExit(Collision other){
		//If Doctor is not colliding with the object anymore, he cant move it anymore and we lose its reference
		if (other.gameObject.tag == "MovableObject") {
			canMoveObject = false;
			movableObject = null;
		}
	}




}
