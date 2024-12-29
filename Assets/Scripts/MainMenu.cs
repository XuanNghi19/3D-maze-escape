using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  private AudioSource audioSource;
  public AudioClip bgm;
  void Start()
  {
    audioSource = gameObject.AddComponent<AudioSource>();
    audioSource.loop = true;
    audioSource.clip = bgm;
    audioSource.Play();
  }
  public void Play()
  {
    SceneManager.LoadScene(2);
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();
  } 

  public void Continue()
  {
    SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
  }

  public void Quit() 
  {
    Application.Quit();
    Debug.Log("Player has quit");
  }

  public void Tutorial()
  {
    SceneManager.LoadScene(1);
  } 

  public void Menu()
  {
    SceneManager.LoadScene(0);
  }
}


