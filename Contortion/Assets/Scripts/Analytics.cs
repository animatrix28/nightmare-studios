using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPressAnalytics : MonoBehaviour
{
    private int countFPresses = 0;
    private string analyticsFilePath;

    void Start()
    {

        analyticsFilePath = Application.persistentDataPath + "/KeyPressAnalytics.txt";

         if (!File.Exists(analyticsFilePath))
        {
            File.WriteAllText(analyticsFilePath, "Analytics for Key Presses\n");
        }

        File.AppendAllText(analyticsFilePath, "\nStarting analytics for: " + SceneManager.GetActiveScene().name + "\n");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            countFPresses++;
            Debug.Log("F key pressed " + countFPresses + " times");


            AppendKeyPressToFile();
        }
    }

    void AppendKeyPressToFile()
    {

        string content = "Level: " + SceneManager.GetActiveScene().name + " - F key pressed " + countFPresses + " times.\n";
        File.AppendAllText(analyticsFilePath, content);
    }

    private void OnDisable()
    {
        File.AppendAllText(analyticsFilePath, "Ending analytics for: " + SceneManager.GetActiveScene().name + " - Total F key presses: " + countFPresses + "\n");
    }
}