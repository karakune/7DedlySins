using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBellAnimation : MonoBehaviour {

    protected void animate()
    {
        GetComponent<Animation>().Play("bellring");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Jester")
        {
            CancelInvoke();
            animate();
        }
    }

    private void Start()
    {
        GetComponent<Animation>().wrapMode = WrapMode.Once;
    }
}
