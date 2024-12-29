using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Player player;
    [SerializeField] private Slider slider;


    void Start()
    {
        slider.maxValue = player.maxHp;
        slider.value = player.hp;
    }

    void Update()
    {
        slider.value = player.hp;
    }

    public void setHealth(int health) {
        slider.value = health;
    }

    public void setMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }
}
