using UnityEngine;

public class GravityToggle : MonoBehaviour
{
    public ReverseGravity[] gravityAffectedBlocks;
    public static string IsPowerUpUsed = "Not Exist";
    public static bool IsPowerUpPresent = false;
    void Start()
    {
        IsPowerUpPresent = true;
        IsPowerUpUsed = "Not Used";
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
            Destroy(gameObject);
        }
    }
}