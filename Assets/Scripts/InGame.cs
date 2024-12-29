using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Shop;
    public GameObject PauseCanvas;
    public GameObject LoseCanvas;
    public GameObject WinCanvas;
    private bool isShopOpen = false;
    private bool isPaused = false;
    public Player player;
    public TextMeshProUGUI coinText;
    public AudioSource MusicSrc;
    public AudioSource SFXSrc;
    public AudioClip upgradeSnd;
    public HeathBar heathBar;
    public Slider musicSlider;
    public Slider sfxSlider;
    private bool hideCursor = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Debug.Log("music volume: " + PlayerPrefs.GetFloat("MusicVolume"));
            Debug.Log("sfx volume: " + PlayerPrefs.GetFloat("SFXVolume"));
            MusicSrc.volume = PlayerPrefs.GetFloat("MusicVolume");
            SFXSrc.volume = PlayerPrefs.GetFloat("SFXVolume");
            musicSlider.value = MusicSrc.volume;
            sfxSlider.value = SFXSrc.volume;
        } else {
            PlayerPrefs.SetFloat("MusicVolume", 1f);    
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            MusicSrc.volume = 1f;
            SFXSrc.volume = 1f;
            musicSlider.value = 1f;
            sfxSlider.value = 1f;
        }
        UpdateCoinText(); // Cập nhật số coin khi bắt đầu
    }

    void Update()
    {
        // Khi nhấn phím P để mở/đóng shop
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isShopOpen)
                HideShop();
            else
                ShowShop();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPaused)
                HidePauseCanvas();
            else
                ShowPauseCanvas();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(hideCursor)
            {
                ShowCursor();
                hideCursor = false;
            }
            else
            {
                HideCursor();
                hideCursor = true;
            }
        }

        // Khi player chết
        if (player.isDead)
        {
            ShowLoseCanvas();
        }

        if (player.isWin && SceneManager.GetActiveScene().buildIndex == 2)
        {
            ShowWinCanvas();
        }

        // Cập nhật số coin trên màn hình
        UpdateCoinText();
    }

    public void ShowSetting()
    {
        Setting.SetActive(true);
        HidePauseCanvas();
        ShowCursor();
    }

    public void HideSetting()
    {
        Setting.SetActive(false);
        HideCursor();
    }

    public void ShowShop()
    {
        isShopOpen = true;
        player.actionCancel = true;
        Shop.SetActive(true);
        ShowCursor();
    }

    public void HideShop()
    {
        isShopOpen = false;
        player.actionCancel = false; 
        Shop.SetActive(false);
        HideCursor();
    }

    public void ShowPauseCanvas()
    {
        isPaused = true;
        PauseCanvas.SetActive(true);
        ShowCursor();
    }

    public void HidePauseCanvas()
    {
        isPaused = false;
        PauseCanvas.SetActive(false);
        HideCursor();
    }

    public void ShowLoseCanvas()
    {
        LoseCanvas.SetActive(true);
        ShowCursor();
    }

    public void HideLoseCanvas()
    {
        LoseCanvas.SetActive(false);
        HideCursor();
    }

    public void ShowWinCanvas()
    {
        WinCanvas.SetActive(true);
        ShowCursor();
    }

    public void HideWinCanvas()
    {
        WinCanvas.SetActive(false);
        HideCursor();
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quits()
    {
        SceneManager.LoadScene(3);
    }

    private void UpdateCoinText()
    {
        coinText.text = player.coin.ToString();
    }
    public void upgradeHealth()
    {
        if (player.coin >= 100)
        {
            player.maxHp += 4;
            player.hp = player.maxHp;
            player.coin -= 100;
            SFXSrc.PlayOneShot(upgradeSnd);
            heathBar.setMaxHealth(player.maxHp);
        }
    }

    public void upgradeDamage()
    {
        if (player.coin >= 100)
        {
            player.atk += 2;
            player.coin -= 100;
            SFXSrc.PlayOneShot(upgradeSnd);
        }
    }

    public void upgradeDef()
    {
        if (player.coin >= 100)
        {
            player.def += 1;
            player.coin -= 100;
            SFXSrc.PlayOneShot(upgradeSnd);
        }
    }

    public void ChangeMusicVolume()
    {
        MusicSrc.volume = musicSlider.value;
        SaveSetting();
    }

    public void ChangeSFXVolume()
    {
        SFXSrc.volume = sfxSlider.value;
        SaveSetting();
    }

    private void SaveSetting()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }
}
