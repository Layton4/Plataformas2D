using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{

    float timeSinceLastDestruction = 0.0f;
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(timeSinceLastDestruction > 3f)
        {
            LevelGenerator.sharedInstance.AddLevelBlock();
            LevelGenerator.sharedInstance.RemoveOldestBlock();
            timeSinceLastDestruction = 0.0f;
        }
    }

    private void Update()
    {
        timeSinceLastDestruction += Time.deltaTime;
    }
}
