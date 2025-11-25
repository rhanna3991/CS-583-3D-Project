using UnityEngine;

// Destroys objects that enter the center of the tornado
public class TornadoCenter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of objects that can be sucked in and destroyed")]
    public string suckableTag = "Suckable";
    [Tooltip("How much XP the tornado gains per object eaten")]
    public float xpPerObject = 10f;

    private TornadoGrowth growthScript;

    void Start()
    {
        growthScript = GetComponentInParent<TornadoGrowth>();
    }

    // Called when another object enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Only destroy objects with the suckable tag
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

