using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D enemyRigidbody;

    public static bool turnArround;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float currentSpeed = runningSpeed;

        if(turnArround == true)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            currentSpeed = -runningSpeed;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            currentSpeed = runningSpeed;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
                enemyRigidbody.velocity = new Vector2(currentSpeed, enemyRigidbody.velocity.y);
        }
    }
}
