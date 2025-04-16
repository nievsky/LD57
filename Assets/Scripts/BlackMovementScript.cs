using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackMovementScript : MonoBehaviour
{
    public Transform player;
    public float delay = 5f; 
    public float acceleration = 5f;  
    public float maxSpeed = 10f;     
    public float snapDistance = 0.1f; 

    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        if (player == null)
            return;
        
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = player.position;
        Vector2 toTarget = targetPosition - currentPosition;
        float distanceToTarget = toTarget.magnitude;
        
        if (distanceToTarget < snapDistance)
        {
            transform.position = targetPosition;
            velocity = Vector2.zero;
            return;
        }
        
        Vector2 targetDirection = toTarget.normalized;
        
        velocity += targetDirection * acceleration * Time.deltaTime;
        
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        
        transform.position = currentPosition + velocity * Time.deltaTime;
        StartCoroutine(DestroyRoutine());
    }
    
    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
