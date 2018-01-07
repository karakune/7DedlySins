using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject monster;
	public GameObject doctor;
	public GameObject jester;
	public AudioSource stressSound;
	public float minDistanceStress;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("StressSoundPlayer");
	}

	void StressSoundPlayer(){
		float distance = Vector3.Distance (monster.transform.position, doctor.transform.position);
		if (minDistanceStress> distance) {
			stressSound.volume = 5.0f/distance;
		}
		else{
			stressSound.volume = 0.1f;
		}

	}
}
