using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public string restartSceneName = "Map";
    public TextMeshProUGUI instructionsText;

    void Start()
    {
        if (instructionsText == null)
        {
            instructionsText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (instructionsText != null)
        {
            instructionsText.text = "YOU WON!!!\n\nPress R to Restart\nPress M for Main Menu\nPress ESC to Quit";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GoToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(restartSceneName))
        {
            SceneManager.LoadScene(restartSceneName);
        }
    }

    public void GoToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
