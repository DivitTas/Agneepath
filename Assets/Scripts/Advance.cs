using UnityEngine;
using UnityEngine.SceneManagement;

public class Advance : MonoBehaviour
{
    public string nextSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is empty! Set it in the Inspector.");
        }
    }
}
