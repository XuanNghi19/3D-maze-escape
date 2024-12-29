using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{

[SerializeField] private Slider slider;

    public void setHealth(int health) {
        slider.value = health;
    }
}
