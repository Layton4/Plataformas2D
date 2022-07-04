using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    public float jumpForce = 50f;
    public float runningSpeed = 10f;

    public LayerMask groundLayer; //variable para detectar la capad el suelo
    public Animator PlayerAnimator;

    private Rigidbody2D PlayerRigidbody;
    private float raycastDistance = 0.2f;

    private Vector3 startPosition = new Vector3(0, 0, 0);

    private int healthPoints, manaPoints;
    void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        if (sharedInstance == null)
        {
            // Configuramos la instancia
            sharedInstance = this;
            // Nos aseguramos de que no sea destruida con el cambio de escena
           
        }
        else
        {
            // Como ya existe una instancia, destruimos la copia
            Destroy(this);
        }
        startPosition = transform.position;
    }

    public void StartGame()
    {
        PlayerAnimator.SetBool("isGrounded", true);
        PlayerAnimator.SetBool("isAlive", true);
        transform.position = startPosition;
        healthPoints = 150;
        manaPoints = 25;
        StartCoroutine("TiredPlayer");
    }

    void Update()
    {
        
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) //Al pulsar la tecla de flechaArriba
            {
                Jump(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(true);
            }
            PlayerAnimator.SetBool("isGrounded", IsOnTheGround());
        }
            
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (PlayerRigidbody.velocity.x < runningSpeed)
                {
                    PlayerRigidbody.velocity = new Vector2(runningSpeed, PlayerRigidbody.velocity.y);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (PlayerRigidbody.velocity.x > -runningSpeed)
                {
                    PlayerRigidbody.velocity = new Vector2(-runningSpeed, PlayerRigidbody.velocity.y);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                }
            }
        }
    }

    /*void Jump()
    {
        if(IsOnTheGround())
        {
            PlayerAnimator.SetBool("isGrounded", false);
            PlayerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    */
    void Jump(bool isSuperJump)
    {
        if(IsOnTheGround())
        {
            if (isSuperJump && manaPoints >= 3)
            {
                manaPoints -= 3;
                PlayerRigidbody.AddForce(Vector2.up * jumpForce * 1.5f, ForceMode2D.Impulse);
            }
            else
            {
                PlayerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }

    bool IsOnTheGround() //Devolverá true si estamos en el suelo y false en caso contrario
    {
        if(Physics2D.Raycast(transform.position,Vector2.down,raycastDistance,groundLayer)) //trazamos un rayo desde el player hacia abajo, hasta 20 cm de distancia si toca el suelo entonces
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        PlayerAnimator.SetBool("isAlive", false);
        StopAllCoroutines();
    }

    public float GetDistance()
    {
        float traveledDistance = Vector2.Distance(new Vector2(startPosition.x,0), new Vector2(transform.position.x,0));
        return traveledDistance;
    }

    public void CollectHealth(int points)
    {
        healthPoints += points;
        if(healthPoints >= 150)
        {
            healthPoints = 150;
        }
    }
    public void ColectMana(int points)
    {
        manaPoints += points;
        if(manaPoints >= 25)
        {
            manaPoints = 25;
        }
    }

    IEnumerator TiredPlayer()
    {
        while(healthPoints > 0)
        {
            healthPoints--;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public int GetHealth()
    {
        return healthPoints;
    }
    public int GetMana()
    {
        return manaPoints;
    }
}
