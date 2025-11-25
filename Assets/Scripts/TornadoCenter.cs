using UnityEngine;

// Destroys objects that enter the center of the tornado
public class TornadoCenter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of objects that can be sucked in and destroyed")]
    public string suckableTag = "Suckable";
    public string suckableSmall = "SuckableSmall";

    public string suckableLarge = "SuckableLarge";

    public string suckableMedium = "SuckableMedium";


    // Called when another object enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Only destroy objects with the suckable tag
        if (other.CompareTag(suckableTag) || other.CompareTag(suckableSmall) || other.CompareTag(suckableMedium) || other.CompareTag(suckableLarge))
        {
            Destroy(other.gameObject);
        }
    }
}

