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
using System;
using UnityEngine.SceneManagement;
using Proyecto26; // Make sure RestClient is correctly imported

public class RotationAnalytics : MonoBehaviour
{
    private int rotationCount = 0;
    // private string firebaseURL = "https://contortion-6c4d5-default-rtdb.firebaseio.com/rotationAnalytics.json";
    //private string firebaseURL = "https://contortion-6c4d5-default-rtdb.firebaseio.com/rotationAnalyticsTESTING.json";

    // //UNCOMMENT THIS WHEN BUILDING
    private string firebaseURL = "https://contortion-6c4d5-default-rtdb.firebaseio.com/rotationAnalyticsGold.json";
    // private string firebaseURL = ""; // testing locally

    void OnEnable()
    {
        RotatePlayArea.OnRotationStart += RotationStarted;
    }

    void OnDestroy()
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
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string causeOfDeath = PlayerKiller.CauseOfDeath;
        string levelStatus = LevelChange.LevelStatus;
        // string gravityToggleUsed = GravityToggle.IsPowerUpUsed;
        // string gravityToggleUsed = GravityToggle.IsPowerUpPresent ? GravityToggle.IsPowerUpUsed :"Not Exist";
        string gravityToggleUsed = PlayerPrefs.GetString("IsPowerUpUsed", "Not Exist");

        RotationData data = new RotationData
        {
            level = levelName,
            rotations = rotationCount,
            date = timestamp,
            causeOfDeath = causeOfDeath,
            levelStatus = levelStatus,
            gravityToggleUsed = gravityToggleUsed
        };

        // Debug.Log(causeOfDeath+"Analytics");
        // Send data to Firebase using RestClient
        RestClient.Post(firebaseURL, data).Then(response =>
        {
            Debug.Log("Data successfully sent to Firebase!");
            LevelChange.LevelStatus = "Unknown";
            PlayerKiller.CauseOfDeath = "Unknown";
            // GravityToggle.IsPowerUpUsed = "Not Exist";
            // GravityToggle.IsPowerUpPresent = false;
            PlayerPrefs.SetString("IsPowerUpUsed", "Not Exist");
            PlayerPrefs.SetInt("IsPowerUpPresent", 0);
            PlayerPrefs.Save();
        }).Catch(error =>
        {
            Debug.LogError("Error sending data to Firebase: " + error);
        });
    }

    [Serializable]
    public class RotationData
    {
        public string level;
        public int rotations;
        public string date;
        public string causeOfDeath;
        public string levelStatus;
        public string gravityToggleUsed;
    }
}