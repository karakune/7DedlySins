using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public List<Vector3> EnemyCheckpoints;
    private List<Vector3> DoctorCheckpoints;
    private List<Vector3> JesterCheckpoints;

    public GameObject Doctor;
    public GameObject Jester;
    public GameObject Enemy;

    public int currentCheckPoint = 0;

    public void UnLoadLastCheckPoint()
    {
        Doctor.transform.position = DoctorCheckpoints[this.currentCheckPoint];
        Jester.transform.position = JesterCheckpoints[this.currentCheckPoint];
        Enemy.transform.position = EnemyCheckpoints[this.currentCheckPoint];
    }

    public void ChangeCheckPoint()
    {
        this.currentCheckPoint++;
        LoadCheckPoint();
    }

    private void LoadCheckPoint()
    {
        DoctorCheckpoints[this.currentCheckPoint] = Doctor.transform.position;
        JesterCheckpoints[this.currentCheckPoint] = Jester.transform.position;
    }

    public void ResetCheckPoints()
    {
        this.currentCheckPoint = 0;
    }

    private void Start()
    {
        LoadCheckPoint();
    }
}
