using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D OtherTrigger)
    {
        if(OtherTrigger.gameObject.CompareTag("Player"))
        {
            PlayerController.sharedInstance.Kill();
        }
    }
}
