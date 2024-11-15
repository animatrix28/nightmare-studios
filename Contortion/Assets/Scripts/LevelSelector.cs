using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Import TextMeshPro namespace
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public TMP_Dropdown levelDropdown;  // TextMeshPro Dropdown
    public Button playButton;

    void Start()
    {
        // Assign the button click event
        playButton.onClick.AddListener(PlayButtonClick);

        // Optional: Dynamically populate the dropdown
        PopulateDropdown();
    }

    void PlayButtonClick()
    {
        int selectedLevel = levelDropdown.value;  // Get selected dropdown value
        string sceneName = GetSceneName(selectedLevel);

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);  // Load the selected scene
        }
        else
        {
            Debug.LogWarning("Selected level does not exist.");
        }
    }

    string GetSceneName(int index)
    {
        // Map dropdown indices to scene names
        switch (index)
        {
            case 0: return "Tutorial_1";
            case 1: return "Level_1";
            case 2: return "Level_2";
            case 3: return "Level_3";
            case 4: return "Level_4";
            case 5: return "Level_5";
            case 6: return "Level_6";
            default: return null;
        }
    }

    void PopulateDropdown()
    {
        // Clear existing options
        levelDropdown.ClearOptions();

        // Create a list of level names
        var options = new System.Collections.Generic.List<string>
        {
            "Tutorial",
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5",
            "Level 6"
        };

        // Add options to the dropdown
        levelDropdown.AddOptions(options);
    }
}
