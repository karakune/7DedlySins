using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour {

    public string nextScene;
    public bool isEnd;
    public bool cantUseStart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Levels")))
        {
            if (Input.GetButton("Start"))
                SceneManager.LoadScene(nextScene);
        }
        else if(isEnd)
        {
            if (Input.GetButton("Back"))
                Application.Quit();
        }  
	}
}
