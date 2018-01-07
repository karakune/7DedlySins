using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUIController : MonoBehaviour {

    public GameObject canvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Back"))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
	}
}
