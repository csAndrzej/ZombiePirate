using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public int CurrentHP;
    public Slider slider;

    public GameObject DiedScreen;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void Update()
    {
        slider.value = PlayerController2D.Health;

        if (PlayerController2D.Health <= 0)
        {
            DiedScreen.SetActive(true);
        }
    }

    
    
}

