using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject Shop; // Canvas chính
    [SerializeField] private GameObject Pause;   // Canvas tạm dừng
    [SerializeField] private GameObject Lose;   // Canvas tạm dừng
    [SerializeField] private GameObject Setting;   // Canvas tạm dừng
    [SerializeField] private GameObject Win;   // Canvas tạm dừng
    [SerializeField] private GameObject HUD;   // Canvas tạm dừng

    private bool isPaused = true;  

    public void ShowPauseCanvas()
    {
        isPaused = true;
        Pause.SetActive(true);   // Bật Canvas tạm dừng
        Time.timeScale = 0f; // Dừng thời gian trong game
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

   
    // Update is called once per frame
    public void HidePauseCanvas()
    {
        isPaused = false;
        Pause.SetActive(false);   // Tắt Canvas tạm dừng
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
    }

     public void ShowSetting()
    {
        Setting.SetActive(true);
    }
    public void HideSetting()
    {
        Setting.SetActive(false);
    }

    // public void Lose()
    // {
    //     Lose.SetActive(true);   
    //     Time.timeScale = 0f; 
    // }
    // public void Win()
    // {
    //     Pause.SetActive(false);   
    //     Time.timeScale = 0f; 
    // }

    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                HidePauseCanvas();
            else
                ShowPauseCanvas();
        }
    }
}
