using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe exécutant un bouton qui doit trigger la descente ou la montée d'un mur
 */
public class TriggerWall : MovementVerticalAnimation {

    public GameObject affectedWall;
    public bool isForever;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Doctor")
        {
            InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
            affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Doctor")
        {
            if (!isForever)
            {
                InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
                affectedWall.GetComponent<MoveWallGroup>().triggerMove = true;
            }
        }
    }
}
