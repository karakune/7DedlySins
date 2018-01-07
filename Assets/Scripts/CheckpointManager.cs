using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour {

    public List<Vector3> EnemyCheckpoints;
    private List<Vector3> DoctorCheckpoints;
    private List<Vector3> JesterCheckpoints;



    public GameObject Doctor;
    public GameObject Jester;
    public GameObject Enemy;

    public static int currentCheckPoint = 0;
	public static int monsterCheckpoint = 0;
	public static bool gameOver = false;
	private bool spawned = false;


    public void UnLoadLastCheckPoint()
    {
		Enemy.transform.position = EnemyCheckpoints[currentCheckPoint];
    }

    public void ChangeCheckPoint()
    {
        currentCheckPoint++;
        LoadCheckPoint();
    }

	public void LoadCheckPoint()
    {
		Enemy.SetActive (false);
		monsterCheckpoint = 0;
		if (currentCheckPoint == 2) {
			monsterCheckpoint = 2;
		}
		Doctor.transform.position = DoctorCheckpoints[currentCheckPoint];
		Jester.transform.position = JesterCheckpoints[currentCheckPoint];
		Jester.GetComponent<JesterController> ().Start ();
		Doctor.GetComponent<DoctorController> ().Start ();
		gameOver = false;
    }

    public void ResetCheckPoints()
    {
        currentCheckPoint = 0;
    }

    private void Start()
    {
		DoctorCheckpoints = new List<Vector3> ();
		JesterCheckpoints= new List<Vector3> ();
		EnemyCheckpoints= new List<Vector3> ();
		Checkpoint0 ();
		Doctor.transform.position = DoctorCheckpoints[currentCheckPoint];
		Jester.transform.position = JesterCheckpoints[currentCheckPoint];
    }

	void Update(){
		if (currentCheckPoint == 1) {
			Checkpoint1 ();
		} else if (currentCheckPoint == 2) {
			Checkpoint2 ();
		}
		if (gameOver) {
			LoadCheckPoint ();
		}
	}

	void Checkpoint0(){
		DoctorCheckpoints.Add (new Vector3 (-6,0,-6));
		JesterCheckpoints.Add (new Vector3 (-6,1.8f,-4));
		monsterCheckpoint = 0;
	}

	void Checkpoint1(){
		
		DoctorCheckpoints.Add (new Vector3 (10,0,4.5f));
		JesterCheckpoints.Add (new Vector3 (10,1.8f,6.5f));
		if (monsterCheckpoint < 1) {
			Enemy.SetActive (false);
			spawned = false;
		} else if (!spawned && monsterCheckpoint == 1 ) {
			spawned = true;
			Enemy.SetActive (true);
			Enemy.transform.position = new Vector3(15,1,5);
		}
		else if (!spawned && monsterCheckpoint == 2 ) {
			spawned = true;
			Enemy.SetActive (true);
			Enemy.transform.position = new Vector3(50,1,6);
		}

	}

	void Checkpoint2(){
		DoctorCheckpoints.Add (new Vector3 (49,0,4.5f));
		JesterCheckpoints.Add (new Vector3 (49,1.8f,6.5f));
		if (monsterCheckpoint < 3) {
			Enemy.SetActive (false);
			spawned = false;
		}else if (!spawned && monsterCheckpoint == 3 ) {
			spawned = true;
			Enemy.SetActive (true);
			Enemy.transform.position = new Vector3(50,1,5);
		}
		/*else if (!spawned && monsterCheckpoint == 4 ) {
			spawned = true;
			Enemy.SetActive (true);
			Enemy.transform.position = new Vector3(50,1,6);
		}*/
	}






}
