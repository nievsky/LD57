using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(0, player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
