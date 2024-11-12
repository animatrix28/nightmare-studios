using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;

public class TutorialTextManager : MonoBehaviour
{
    public GameObject tutorialMessage; // Reference to the Canvas
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshPro component

    void Start()
    {
        if (tutorialMessage != null && tutorialText != null)
        {
            StartCoroutine(ShowTutorialText());
        }
    }

    IEnumerator ShowTutorialText()
    {
        tutorialMessage.SetActive(true); // Show the canvas
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        tutorialMessage.SetActive(false); // Hide the canvas
    }
}
