using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Proyecto26; // Make sure RestClient is correctly imported

public class RotationAnalytics : MonoBehaviour
{
    private int rotationCount = 0;
    private string firebaseURL = "https://contortion-6c4d5-default-rtdb.firebaseio.com/rotationAnalytics.json";

    public static event Action OnRotationStart;

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
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        RotationData data = new RotationData
        {
            level = levelName,
            rotations = rotationCount,
            date = timestamp
        };

        // Send data to Firebase using RestClient
        RestClient.Post(firebaseURL, data).Then(response =>
        {
            Debug.Log("Data successfully sent to Firebase!");
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
    }
}
