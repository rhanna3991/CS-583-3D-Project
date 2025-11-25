using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;

    [Header("Visual Settings")]
    public float spinSpeed = 600f; // How fast the tornado spins

    void Update()
    {
        // 1. Get Input (WASD or Arrow Keys)
        float x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        // 2. Calculate Direction
        // Normalize so moving diagonally isn't faster than moving straight
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        // 3. Move the Player
        // Check magnitude to stop calculations if we aren't pressing keys
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}