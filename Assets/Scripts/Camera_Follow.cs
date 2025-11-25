using UnityEngine;

public class CameraFixedAngleFollow : MonoBehaviour
{
    // The object to follow
    public Transform target;

    // Height above the target
    public float height = 15f;
    // Distance behind the target
    public float distance = 25f;
    // Camera tilt angle in degrees
    public float tiltAngle = 40f;

    // How fast the camera follows (higher = faster)
    public float followSmooth = 5f;

    // Called after all Update methods, ensures smooth camera movement
    void LateUpdate()
    {
        // Exit if no target is assigned
        if (!target) return;

        // Calculate offset position: behind and above the target
        Vector3 offset =
            (-Vector3.forward * distance) +
            (Vector3.up * height);

        // Calculate where the camera should be
        Vector3 desiredPos = target.position + offset;

        // Smoothly move camera to desired position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            followSmooth * Time.deltaTime
        );

        // Set camera rotation to fixed tilt angle
        transform.rotation = Quaternion.Euler(
            tiltAngle,
            0f,
            0f
        );
    }
}
