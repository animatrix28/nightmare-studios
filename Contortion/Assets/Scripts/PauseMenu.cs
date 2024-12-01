using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Assign the PauseMenu Canvas in the Inspector
    public GameObject pauseButton; // Assign the PauseButton in the Inspector
    private bool isPaused = false;

    public void TogglePauseMenu()
    {
        // Log the current state for debugging
        Debug.Log("TogglePauseMenu called. isPaused: " + isPaused);

        if (isPaused)
        {
            Debug.Log("RESUME FUNCTION CALLED");
            ResumeGame();
        }
        else
        {
            Debug.Log("PAUSE FUNCTION CALLED");
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;            // Update the state immediately
        Debug.Log("Resume button clicked: " + isPaused);
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        pauseButton.SetActive(true);   // Show the pause button
        Time.timeScale = 1f;           // Resume the game
    }

    public void PauseGame()
    {
        isPaused = true;             // Update the state immediately
        Debug.Log("Pause button clicked: " + isPaused);
        pauseButton.SetActive(false); // Immediately hide the button
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;          // Pause the game
    }
    
    public void RestartGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        pauseButton.SetActive(true);   // Show the pause button
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Ensure normal time before changing scenes
        SceneManager.LoadScene("Menu"); // Replace with your main menu scene name
    }
}
