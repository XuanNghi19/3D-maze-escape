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

    void OnTriggerStay(Collider other)
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
            // Disable the monster
            if(transform.Find("Ghost") != null)
            {
                GameObject monsterMesh = transform.Find("Ghost").gameObject;
                monsterMesh.SetActive(false);
            }
            if(transform.Find("Tower") != null)
            {
                GameObject monsterMesh = transform.Find("Tower").gameObject;
                monsterMesh.SetActive(false);
            }
            if(transform.Find("Boss") != null)
            {
                GameObject monsterMesh = transform.Find("Boss").gameObject;
                monsterMesh.SetActive(false);
            }
            audioSrc.PlayOneShot(dieSound);
            Debug.Log("Monster is dead!");
            monsterCollider.enabled = false;
            yield return new WaitForSeconds(dieSound.length);
            Destroy(gameObject);
        }
    }
}
