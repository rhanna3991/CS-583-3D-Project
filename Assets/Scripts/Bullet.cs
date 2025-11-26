using UnityEngine;
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Tooltip("How fast the bullet moves")]
    public float speed = 10f;
    
    [Tooltip("How much XP the tornado loses when hit")]
    public float xpDamage = 20f;
    
    [Tooltip("How long the bullet exists before destroying itself")]
    public float lifetime = 5f;
    
    private float spawnTime;
    private GameObject shooter; // The enemy that shot this bullet

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;
        
        // Destroy after lifetime expires
        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetShooter(GameObject shooterObject)
    {
        shooter = shooterObject;
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Ignore collision with the enemy that shot this bullet
        if (shooter != null && other.gameObject == shooter)
        {
            return;
        }
        
        // Check if we hit the tornado by looking for TornadoGrowth component
        TornadoGrowth growthScript = other.GetComponentInParent<TornadoGrowth>();
        if (growthScript == null)
        {
            // Try finding it on the same object
            growthScript = other.GetComponent<TornadoGrowth>();
        }
        
        if (growthScript != null)
        {
            growthScript.RemoveExperience(xpDamage);
            
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}

