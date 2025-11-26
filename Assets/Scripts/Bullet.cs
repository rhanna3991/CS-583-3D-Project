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
    
    [Header("Effects")]
    [Tooltip("Explosion prefab to spawn on contact")]
    public GameObject explosionPrefab;
    
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
            
            // Spawn explosion effect at impact point
            SpawnExplosion(transform.position);
            
            // Destroy the bullet
            Destroy(gameObject);
        }
        // Check if we hit an enemy
        else if (other.GetComponent<EnemyAI>() != null)
        {
            // Spawn explosion effect at impact point
            SpawnExplosion(transform.position);
            
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
    
    void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            
            // Auto-destroy the explosion after its particle system finishes
            ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                // Get the main duration of the particle system
                float duration = ps.main.duration + ps.main.startLifetime.constantMax;
                Destroy(explosion, duration);
            }
            else
            {
                // Destroy after 1 second if no particle system found
                Destroy(explosion, 1f);
            }
        }
    }
}

