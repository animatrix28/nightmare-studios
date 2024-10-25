//DO NOT REMOVE, CODE FOR LOCAL LOGGING

// using UnityEngine;
// using System.IO;
// using UnityEngine.SceneManagement;

// public class RotationAnalytics : MonoBehaviour
// {
//     private int rotationCount = 0;
//     private string analyticsFilePath;
//     public static event System.Action OnRotationStart;
//     void Start()
//     {

//         string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
//         analyticsFilePath = desktopPath + "/RotationAnalytics.txt";


//         if (!File.Exists(analyticsFilePath))
//         {

//             File.WriteAllText(analyticsFilePath, "Total Rotation Count Log\n");


//         }
//     }

//     void OnEnable()
//     {
//         RotatePlayArea.OnRotationStart += RotationStarted;
//     }

//     void OnDisable()
//     {
//         RotatePlayArea.OnRotationStart -= RotationStarted;
//         LogFinalRotationCount();
//     }

//     private void RotationStarted()
//     {
//         rotationCount++;
//     }

//     private void LogFinalRotationCount()
//     {

//         using (StreamWriter writer = new StreamWriter(analyticsFilePath, true))
//         {
//             string levelName = SceneManager.GetActiveScene().name;
//             writer.WriteLine($"Total rotations at end of level '{levelName}': {rotationCount} as of {System.DateTime.Now}");
//         }
//     }
// }

using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RotationAnalytics : MonoBehaviour
{
    private int rotationCount = 0;
    public static event System.Action OnRotationStart;

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
        string levelName = SceneManager.GetActiveScene().name;
        AnalyticsResult result = Analytics.CustomEvent("rotation_data", new Dictionary<string, object>
        {
            { "level_name", levelName },
            { "total_rotations", rotationCount },
            { "timestamp", System.DateTime.Now.ToString() }
        });

        Debug.Log("Analytics result: " + result + " at level " + levelName + " with rotations: " + rotationCount);
    }
}
