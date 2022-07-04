using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movingForward;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(movingForward == true)
        {
            Enemy.turnArround = true;
        }
        else
        {
            Enemy.turnArround = false;
        }

        movingForward = !movingForward;
    }
}
