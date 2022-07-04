using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health,
    mana
}
public class PlayerBar : MonoBehaviour
{
    private Slider slider;

    public BarType type;
    void Start()
    {
        slider = GetComponent<Slider>();
    }


    void Update()
    {
        switch(type)
        {
            case BarType.health:
                slider.value = PlayerController.sharedInstance.GetHealth();
            break;

            case BarType.mana:
                slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}
