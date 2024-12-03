using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Button skipButton;  // Assign in Inspector

    void Start()
    {
        // Check if the tutorial has been completed
        bool tutorialCompleted = PlayerPrefs.GetInt("TutorialCompleted", 0) == 1;

        // Show or hide the Skip Tutorial button
        skipButton.gameObject.SetActive(tutorialCompleted);
    }
}
