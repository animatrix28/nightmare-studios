using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextSceneName = $"Level_{nextSceneIndex + 1}";
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene("PlayAgain");
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level_1");
    }
}
