using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}
public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

    bool isCollected = false;
    public int value = 1;
   void Show() //activar la moneda y su collider
    {
        //Activamos la imagen del recolectable
        GetComponent<SpriteRenderer>().enabled = true;
        //Activamos el collider de la moneda
        GetComponent<BoxCollider2D>().enabled = true;
        isCollected = false; //al enseñar la moneda no hemos cogido la moneda
    }

    void Hide() //desactivar la moneda y su collider
    {
        //Dectivamos la imagen del recolectable
        GetComponent<SpriteRenderer>().enabled = false;
        //Desactivamos el collider de la moneda
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Collect()
    {
        isCollected = true;
        Hide();

        switch(type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(value);
                break;
            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.ColectMana(value);
                break;
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D OtherCollider)
    {
        if(OtherCollider.gameObject.CompareTag("Player"))
        {
            Collect();

        }
    }
}
