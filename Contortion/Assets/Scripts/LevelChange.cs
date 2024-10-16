using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelChange : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level_3");
        }
    }
}
