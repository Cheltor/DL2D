using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign your player object to this in the Inspector
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Set this offset to adjust the camera’s position relative to the player

    void FixedUpdate()
    {
        // Calculate the desired position of the camera (including offset)
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the current camera position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera position to follow the player both horizontally and vertically
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
