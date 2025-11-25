using UnityEngine;

public class CameraFixedAngleFollow : MonoBehaviour
{
    // The object to follow
    public Transform target;

    [Header("Base Settings")]
    // Height above the target
    public float height = 15f;
    // Distance behind the target
    public float distance = 25f;
    // Camera tilt angle in degrees
    public float tiltAngle = 40f;

    // How fast the camera follows (higher = faster)
    public float followSmooth = 5f;

    private float initialPlayerSize = 1f;

    [SerializeField] private float currentHeightRuntime;
    [SerializeField] private float currentDistanceRuntime;

    void Start()
    {
        if (target)
        {
            initialPlayerSize = target.localScale.y;
        }
    }

    // Called after all Update methods, ensures smooth camera movement
    void LateUpdate()
    {
        // Exit if no target is assigned
        if (!target) return;

        float currentScale = target.localScale.y;
        float growthDelta = currentScale - initialPlayerSize;

        // Height growth ratio is 3x the distance growth ratio
        float addedHeight = (growthDelta / 0.1f) * 3f;

        // Calculate the target height and distance
        float targetHeight = height + addedHeight;
        float baseHeight = Mathf.Max(height, 0.001f);
        float ratio = targetHeight / baseHeight;
        float targetDistance = distance * ratio;

        // Debug for current height and distance in game view
        currentHeightRuntime = targetHeight;
        currentDistanceRuntime = targetDistance;

        // Calculate offset position: behind and above the target
        Vector3 offset =
            (-Vector3.forward * targetDistance) +
            (Vector3.up * targetHeight);

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
