using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioSource MusicSrc;
    public AudioSource SFXSrc;
    public AudioClip bgm, victory, gameOver;
    public Player player;
    private bool isVictory = false;
    private bool isGameOver = false;
    public GameObject portalEffect;

    void Start()
    {

        // Play background music and set it to loop
        PlayBackgroundMusic();
    }

    void Update()
    {
        if (player.isDead && !isGameOver)
        {
            PlayGameOver();
        }

        if (player.isWin && !isVictory)
        {
            StartCoroutine(PlayVictory());
        }
    }

    void PlayBackgroundMusic()
    {
        MusicSrc.clip = bgm;
        MusicSrc.loop = true; // Set the audio to loop
        MusicSrc.Play();
    }

    public IEnumerator PlayVictory()
    {
        // Set the victory flag to true and play the victory sound
        isVictory = true;
        MusicSrc.Stop(); // Stop the background music
        MusicSrc.loop = false; // Ensure the victory sound does not loop
        player.Victory(); // Call the victory animation
        SFXSrc.PlayOneShot(victory);
        portalEffect.SetActive(true); // Activate the portal effect
        yield return new WaitForSeconds(5f);

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Get the total number of scenes in build settings
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        Debug.Log("Current scene index: " + currentSceneIndex);

        if (currentSceneIndex < totalScenes - 1)
        {
            // Save player data before loading the next scene
            ChangeScene(currentSceneIndex + 1);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("This is the last scene. Game Over!");
        }
    }

    public void PlayGameOver()
    {
        // Set the game over flag to true and play the game over sound
        isGameOver = true;
        MusicSrc.Stop(); // Stop the background music
        MusicSrc.loop = false; // Ensure the game over sound does not loop
        SFXSrc.PlayOneShot(gameOver);

        // Xóa tất cả dữ liệu PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ChangeScene(int sceneIndex)
    {
        player.currentLevel = sceneIndex; // Cập nhật currentLevel trước khi lưu
        player.SavePlayerData();
        SceneManager.LoadScene(sceneIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.LoadPlayerData();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}