using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBellAnimation : MonoBehaviour {

    public float rotationBack;
    public float rotationFront;
    public float speed;
    public float startTime;

    protected void animate()
    {
        Quaternion rotation = gameObject.transform.rotation;
        Quaternion to = new Quaternion(rotation.x + rotationFront, rotation.y, rotation.z, 1);
        
        transform.rotation = Quaternion.Lerp(rotation, to, (Time.time - startTime) * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Doctor")
        {
            startTime = Time.time;
            animate();

        }
    }

    private void Start()
    {
        animate();   
    }
}
