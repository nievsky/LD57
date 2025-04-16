using SingularityGroup.HotReload;
using UnityEngine;

public class NewMovementScript : MonoBehaviour
{
    public float pushStrength = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 pushDir = playerRb.linearVelocity.normalized;
                rb.AddForce(pushDir * pushStrength, ForceMode2D.Impulse);
            }
        }
    }
}
