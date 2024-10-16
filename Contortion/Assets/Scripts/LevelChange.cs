using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int currentLevel = 1;
    public int totalLevels = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");

        if (other.gameObject.tag == "Player")
        {
            if (currentLevel < totalLevels)
            {
                currentLevel++;
                string nextLevel = "Level_" + currentLevel;
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
