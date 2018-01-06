using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterController : MonoBehaviour {

	public float health;
	public float maxDistance;
	public float stunRange;
	public GameObject Doctor;
	public bool healthDecreasing;


	private float timePassed;



	// Use this for initialization
	void Start () {
		healthDecreasing = false;

		timePassed = 0;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		healthDecreasing = true;
		if (Vector3.Distance(transform.position,Doctor.transform.position)<maxDistance){
			if (Physics.Raycast(transform.position,(Doctor.transform.position - transform.position),out hit,maxDistance)){
				if (hit.collider.tag == "Player") {
					healthDecreasing = false;
				}
			} 
		}

		if (healthDecreasing) {
			StartCoroutine ("DecreaseHealth");
		} else {
			StopCoroutine ("DecreaseHealth");
		}

		
	}



	void DecreaseHealth(){
		if (timePassed > 1) {
			health--;
			timePassed = 0;
		}
		timePassed += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		print (other.tag);
		if (other.tag == "Player") {
			healthDecreasing = false;
		}
	}	

	void StunTarger(){
	
	}

	void Die(){
		
	}




}
