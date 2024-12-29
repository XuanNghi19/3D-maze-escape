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
    private bool isShopOpen = false;

    public void ShowPauseCanvas()
    {
        isPaused = true;
        Pause.SetActive(true);  
        Time.timeScale = 0f; 
    }

  
    public void HidePauseCanvas()
    {
        isPaused = false;
        Pause.SetActive(false);   // Tắt Canvas tạm dừng
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
    }

      public void Home()
    {
        SceneManager.LoadScene(0);
    }

     public void ShowSetting()
    {
        Setting.SetActive(true);
    }
    public void HideSetting()
    {
        Setting.SetActive(false);
    }


     public void ShowShop()
    {
        isShopOpen = true;
        Shop.SetActive(true);    
    }

    public void HideShop()
    {
        isShopOpen = false;
        Shop.SetActive(false);  
    }


    private void Update()
    {
        // Khi nhấn phím ESC
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPaused)
                HidePauseCanvas();
            else
                ShowPauseCanvas();
        }

        // Khi nhấn phím P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isShopOpen)
                HideShop();
            else
                ShowShop();
        }
    }
}
