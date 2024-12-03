using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public static string LevelStatus = "Unknown";
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
        if (currentSceneIndex == 3)
        {
            PlayerPrefs.SetInt("TutorialCompleted", 1);  // Save completion status
            PlayerPrefs.Save(); // Ensure data is saved
        }
        if (currentSceneIndex < 3)
        {

            string nextSceneName = $"Tutorial_{currentSceneIndex + 1}";
            // Debug.Log(nextSceneIndex);
            LevelStatus = "Unknown";
            // Debug.Log("CurrentSCENE: "+currentSceneIndex+" NextScence: "+nextSceneIndex);
            // Mark tutorial as completed if on the last tutorial scene
            // Debug.Log(nextSceneName + ":" + nextSceneIndex + ": Current:" + currentSceneIndex);


            SceneManager.LoadScene(nextSceneName);
        }
        // else if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        else if (currentSceneIndex < 9)
        {
            string nextSceneName = $"Level_{currentSceneIndex - 2}";
            // Debug.Log(currentSceneIndex);
            LevelStatus = $"Level_{currentSceneIndex - 3}" + "_cleared";
            SceneManager.LoadScene(nextSceneName);

        }
        else
        {
            LevelStatus = "Level_6_cleared";
            SceneManager.LoadScene("PlayAgain");
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SkipTutorial()
    {
        SceneManager.LoadScene("Level_1");
    }
}
