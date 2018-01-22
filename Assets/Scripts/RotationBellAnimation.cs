using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBellAnimation : MonoBehaviour {

    public void Animate()
    {
        GetComponent<Animation>().Play("bellring");
    }

    private void Start()
    {
        GetComponent<Animation>().wrapMode = WrapMode.Once;
    }
}
