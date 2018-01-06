using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MovementVerticalAnimation {

    public GameObject affectedWall;
    public bool isForever;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("MoveTowardsLow", 0f, 0.05f);
            // TODO to finish to trigger animation of the wall
            //affectedWall.GetComponent<MoveWallGroup>().
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isForever)
            {
                InvokeRepeating("MoveTowardsHigh", 0f, 0.05f);
                // TODO to finish to trigger animation of the wall
            }
        }
    }
}
