using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class RotationAnalytics : MonoBehaviour
{
    private int rotationCount = 0;
    private string analyticsFilePath;
    public static event System.Action OnRotationStart;
    void Start()
    {

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        analyticsFilePath = desktopPath + "/RotationAnalytics.txt";


        if (!File.Exists(analyticsFilePath))
        {

            File.WriteAllText(analyticsFilePath, "Total Rotation Count Log\n");
        }
    }

    void OnEnable()
    {
        RotatePlayArea.OnRotationStart += RotationStarted;
    }

    void OnDisable()
    {
        RotatePlayArea.OnRotationStart -= RotationStarted;
        LogFinalRotationCount();
    }

    private void RotationStarted()
    {
        rotationCount++;
    }

    private void LogFinalRotationCount()
    {

        using (StreamWriter writer = new StreamWriter(analyticsFilePath, true))
        {
            string levelName = SceneManager.GetActiveScene().name;
            writer.WriteLine($"Total rotations at end of level '{levelName}': {rotationCount} as of {System.DateTime.Now}");
        }
    }
}
