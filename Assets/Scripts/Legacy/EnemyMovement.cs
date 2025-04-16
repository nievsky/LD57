using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")] public float moveSpeed = 2f;
    public float homingSpeed = 1f;
    
    [Header("Target Settings")] public Transform target;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.up * moveSpeed;

        if (target != null)
        {
            float dirextionX = target.position.x - transform.position.x;
            Vector3 homing = new Vector3(dirextionX, 0f, 0).normalized * homingSpeed;
            moveDirection += homing;
        }
        
        transform.position += moveDirection * Time.deltaTime;
    }
}
