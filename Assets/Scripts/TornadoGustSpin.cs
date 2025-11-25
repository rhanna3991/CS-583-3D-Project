using UnityEngine;

// Spins the tornado gust mesh
public class TornadoGustSpin : MonoBehaviour
{
    public float speed = 200f;

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
