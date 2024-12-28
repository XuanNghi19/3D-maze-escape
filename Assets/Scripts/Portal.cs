using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Player player;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.hadKey)
        {
            player.isWin = true;
        } else if (other.CompareTag("Player") && !player.hadKey)
        {
            Debug.Log("You need a key to open this door!");
        }
    }
}
