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
	public static bool gameOver = false;
	public int index;

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
		Debug.Log ("Loading");
		Doctor.transform.position = DoctorCheckpoints[currentCheckPoint];
		Debug.Log ("Loading1");
		Jester.transform.position = JesterCheckpoints[currentCheckPoint];
		Debug.Log ("Loading2");
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
		//EnemyCheckpoints.Add (new Vector3 ());
	}

	void Checkpoint1(){
		DoctorCheckpoints.Add (new Vector3 (10,0,4.5f));
		JesterCheckpoints.Add (new Vector3 (10,1.8f,6.5f));
		//EnemyCheckpoints.Add (new Vector3 ());
	}

	void Checkpoint2(){
		DoctorCheckpoints.Add (new Vector3 (49,0,4.5f));
		JesterCheckpoints.Add (new Vector3 (49,1.8f,6.5f));
	}




}
