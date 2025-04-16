using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovementSimple : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float acceleration = 2f;
    public float dashDistance = 5f;
    public float dashCooldown = 2f;
    private float nextDashTime = 0f;

    private Vector2 moveInput;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Apply acceleration in both axes
        float shiftMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        baseSpeed = Mathf.MoveTowards(baseSpeed, 10f * shiftMultiplier, acceleration * Time.deltaTime);

        // Move player
        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * baseSpeed;

        // Dash in both x and y
        if (Time.time >= nextDashTime && Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dashDirection = movement.normalized;
            if (dashDirection.sqrMagnitude == 0f)
                dashDirection = new Vector2(transform.localScale.x < 0f ? -1f : 1f, 0f);

            transform.position += new Vector3(dashDirection.x * dashDistance, dashDirection.y * dashDistance, 0f);
            nextDashTime = Time.time + dashCooldown;
        }
    }
}