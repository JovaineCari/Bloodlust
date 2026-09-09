using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public GameObject gameOverUI;
    
    // Update is called once per frame
    public string SceneName;
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneName);
        Debug.Log("Loading Main Menu...");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneName);
        Debug.Log("Loading Game...");
    }
}
