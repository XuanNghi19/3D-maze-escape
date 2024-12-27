using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{

    public Player player;
    private BoxCollider monsterCollider;
    public int hp = 10;
    public int atk = 1;
    private bool canTakeDamage = true;

    //Audio
    public AudioSource audioSrc;
    public AudioClip swordHit, dieSound;

    void Start()
    {
        player = FindObjectOfType<Player>();
        monsterCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == player.swordCollider && canTakeDamage && player.isAttack)
        {
            canTakeDamage = false;
            audioSrc.PlayOneShot(swordHit);
            StartCoroutine(DamageCooldown());
        }
    }

    // Coroutine to handle the cooldown of the monster taking damage
    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(player.attackCooldown);
        canTakeDamage = true;
        Debug.Log("Sword hit the monster!");
        hp -= player.atk;
        if (hp <= 0)
        {
            audioSrc.PlayOneShot(dieSound);
            Debug.Log("Monster is dead!");
            monsterCollider.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(dieSound.length);
            Destroy(gameObject);
        }
    }
}
