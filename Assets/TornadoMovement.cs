using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float strafeSpeed = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent tipping over
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float strafeInput = Input.GetAxis("Horizontal"); // A/D to move left/right
        float verticalInput = Input.GetAxis("Vertical"); // W/S to move forward/backward

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f; // Keep movement horizontal
        camForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;

        // Combine input with camera directions
        Vector3 move = (camForward * verticalInput + cameraRight * strafeInput).normalized;

        rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);

        if ( move != Vector3.zero )
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, strafeSpeed * Time.fixedDeltaTime));
        }
    }
}
