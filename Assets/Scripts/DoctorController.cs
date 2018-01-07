using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorController : DoctorMover {

	public GameObject jester;
	//Actual and max health
	public float health;
	public float maxHealth;
	//Range of healing ability
	public float healingRange;
	//healing skill cooldown
	public float healingCd;
	public Image healFillClockImage;
	public int frameCounter = 0;

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


	public override void Start () {
		//Call of parent.Start()
		base.Start ();

		maxHealth = 100;
		health = maxHealth;
		healingRange = 5;
		healingCd = 5;
		//Get JesterController script
		jesterController = jester.GetComponent<JesterController>();
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
                anim.SetTrigger("Heal");
                Heal ();
			}

			//If Doctor wants to move object and is colliding with it => the object will follow doctors pos
			if (canMoveObject && Input.GetButtonDown (X)) {
                if (!anim.GetBool("Push"))
                    anim.SetBool("Push", true);
				MoveObject ();
			}
			//When button X up, Detach object from doctor
			if (movableObject!=null && Input.GetButtonUp(X)){
                movableObject.transform.SetParent (null);
				movableObject.GetComponent<Rigidbody> ().isKinematic = true;
                if (anim.GetBool("Push"))
                    anim.SetBool("Push", false);
            }

		}

		if (!canHeal) {
			frameCounter++;
			if (frameCounter == 60) {
				frameCounter = 0;
				UpdateCooldown();
			}
		}

	}

	void Heal(){
		//If jester is seen => heal
		RaycastHit hit;
		if (Physics.Raycast (transform.position, (jester.transform.position - transform.position), out hit, healingRange*2)) {
			jesterController.health = jesterController.maxHealth;
			jesterController.UpdateUIHealth();
			canHeal = false;
			//launch skill cooldown
			StartCoroutine (HealingCD());
		}
	}

	//Move the object by setting kinematic false and by attaching movable object to doctor
	void MoveObject(){
		movableObject.GetComponent<Rigidbody> ().isKinematic = false;
		movableObject.transform.SetParent (transform);
	}


	public void UpdateCooldown()
	{
		healFillClockImage.fillAmount += 0.25f;
	}

	//The docotor can heal again after the healingCd
	IEnumerator HealingCD(){
		healFillClockImage.fillAmount = 0f;
		yield return new WaitForSeconds (healingCd);
		canHeal = true;
	}

	void Die()
    {
        Debug.Log("You died!");
        canMove = false;
		CheckpointManager.gameOver = true;
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

		if (other.gameObject.name == "Ground_Wood_Checkpoint1"){
			CheckpointManager.currentCheckPoint++;
		}	

		if (other.gameObject.name == "Ground_Wood_Checkpoint2"){
			CheckpointManager.currentCheckPoint++;
		}	

		if (other.gameObject.name == "Ground_Wood_Checkpoint3"){
			CheckpointManager.currentCheckPoint++;
		}	

		if (other.gameObject.name == "Ground_CheckpointMonster1"){
			CheckpointManager.monsterCheckpoint++;
		}
		if (other.gameObject.name == "Ground_Wood_CheckpointMonster2"){
			CheckpointManager.monsterCheckpoint++;
		}
		if (other.gameObject.name == "Ground_Wood_CheckpointMonster3"){
			CheckpointManager.monsterCheckpoint++;
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
