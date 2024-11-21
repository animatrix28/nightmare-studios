using UnityEngine;

public class GravityToggle : MonoBehaviour
{
    public ReverseGravity[] gravityAffectedBlocks;
    public static string IsPowerUpUsed = "Not Used";
    public static bool IsPowerUpPresent = false;
    void Start()
    {
        // IsPowerUpPresent = true;
        // IsPowerUpUsed = "Not Used";
        IsPowerUpPresent = PlayerPrefs.GetInt("IsPowerUpPresent", 0) == 1;
        IsPowerUpUsed = PlayerPrefs.GetString("IsPowerUpUsed", "Not Used");

        // Update state for this instance
        IsPowerUpPresent = true;
        IsPowerUpUsed = "Not Used";
        SaveState();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPowerUpUsed = "Used";
            foreach (var block in gravityAffectedBlocks)
            {
                if (block != null)
                {
                    block.ReverseObjectGravity();
                }
            }
            SaveState();
            Destroy(gameObject);
        }
    }
    private void SaveState()
    {
        PlayerPrefs.SetInt("IsPowerUpPresent", IsPowerUpPresent ? 1 : 0);
        PlayerPrefs.SetString("IsPowerUpUsed", IsPowerUpUsed);
        PlayerPrefs.Save();
    }
}