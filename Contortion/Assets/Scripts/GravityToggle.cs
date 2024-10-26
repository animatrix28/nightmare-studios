using UnityEngine;

public class GravityToggle : MonoBehaviour
{
    public ReverseGravity[] gravityAffectedBlocks;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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