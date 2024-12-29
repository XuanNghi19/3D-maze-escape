using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void Play()
  {
    SceneManager.LoadScene(2);
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

  public void BackToMenu()
  {
    SceneManager.LoadScene(0);
  } 


}


