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
        // Debug.Log(currentSceneIndex+"wefwbijuwbi");

        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {

            string nextSceneName = $"Level_{currentSceneIndex}";
            // Debug.Log(nextSceneIndex);
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

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
}
