using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource audioSrc;
    public AudioClip bgm, victory, gameOver;
    public Player player;
    private bool isVictory = false;
    private bool isGameOver = false;
    public GameObject portalEffect;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        // Play background music and set it to loop
        PlayBackgroundMusic();
    }

    void Update()
    {
        if(player.isDead && !isGameOver)
        {
            PlayGameOverSound();
        }

        if(player.isWin && !isVictory)
        {
            StartCoroutine(PlayVictorySound());
        }
    }

    void PlayBackgroundMusic()
    {
        audioSrc.clip = bgm;
        audioSrc.loop = true; // Set the audio to loop
        audioSrc.Play();
    }

    public IEnumerator PlayVictorySound()
    {
        // Set the victory flag to true and play the victory sound
        isVictory = true;
        audioSrc.Stop(); // Stop the background music
        audioSrc.loop = false; // Ensure the victory sound does not loop
        player.Victory(); // Call the victory animation
        audioSrc.PlayOneShot(victory);
        portalEffect.SetActive(true); // Activate the portal effect
        yield return new WaitForSeconds(5/3f);
    }

    public void PlayGameOverSound()
    {
        // Set the game over flag to true and play the game over sound
        isGameOver = true;
        audioSrc.Stop(); // Stop the background music
        audioSrc.loop = false; // Ensure the game over sound does not loop
        audioSrc.PlayOneShot(gameOver);
    }
}
