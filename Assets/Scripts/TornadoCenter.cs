using UnityEngine;

// Destroys objects that enter the center of the tornado
public class TornadoCenter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of objects that can be sucked in and destroyed")]
    public string suckableTag = "Suckable";
    
    [Tooltip("How much XP the tornado gains per object eaten")]
    public float xpPerObject = 10f;
    
    [Header("Enemy Settings")]
    [Tooltip("How much XP the tornado gains per enemy killed")]
    public float xpPerEnemy = 25f;

    private TornadoGrowth growthScript;

    void Start()
    {
        growthScript = GetComponentInParent<TornadoGrowth>();
    }

    // Called when another object enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if it's an enemy and destroy it
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            if (growthScript != null)
            {
                growthScript.AddExperience(xpPerEnemy);
            }

            Destroy(other.gameObject);
            return;
        }
        
        // Check if it's a suckable object with the tag
        if (other.CompareTag(suckableTag))
        {
            if (growthScript != null)
            {
                growthScript.AddExperience(xpPerObject);
            }

            Destroy(other.gameObject);
        }
    }
}

