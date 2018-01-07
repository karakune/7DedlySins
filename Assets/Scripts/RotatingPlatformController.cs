using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MovementRotationAnimation, IPossessable {

    public AudioSource slideSound;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
		
	}

    public void Possess()
    {
        if (isInMotion == false) {
			Debug.Log("was not in motion");
                    slideSound.Play();
            Rotate(RotationDirections.Clockwise);
        }
    }

    public void Glow(Color color)
    {
        Color colorWithOpacity = new Color(color.r, color.g, color.b, 0.5f);
        if(gameObject.transform.Find("Glow"))
        {
            GameObject glowObject = gameObject.transform.Find("Glow").gameObject;
            glowObject.GetComponent<Renderer>().material.color = colorWithOpacity;
            glowObject.SetActive(true);
        }
    }

    public void UnGlow() {
        if (gameObject.transform.Find("Glow"))
        {
            gameObject.transform.Find("Glow").gameObject.SetActive(false);
        }
    }
}
