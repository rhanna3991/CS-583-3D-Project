using UnityEngine;
using System.Collections.Generic;

// Pulls in objects with a specific tag toward the tornado
public class TornadoSuction : MonoBehaviour
{
    [Header("Suction Settings")]
    [Tooltip("Tag of objects that can be sucked in")]
    public string suckableTag = "Suckable";
    
    [Tooltip("How fast objects move toward the tornado")]
    public float suckSpeed = 5f;
    
    [Tooltip("Distance at which objects start being sucked in")]
    public float suckRange = 10f;
    
    [Tooltip("How often to find suckable objects (in seconds). Lower = more accurate but more expensive")]
    public float findInterval = 0.2f;

    private GameObject[] suckableObjects;
    private float lastFindTime = 0f;

    void Update()
    {
        // Periodically find all suckable objects
        if (Time.time - lastFindTime >= findInterval)
        {
            lastFindTime = Time.time;
            suckableObjects = GameObject.FindGameObjectsWithTag(suckableTag);
        }

        // Move objects every frame for smooth movement
        if (suckableObjects != null)
        {
            SuckInObjects();
        }
    }

    void SuckInObjects()
    {
        foreach (GameObject obj in suckableObjects)
        {
            if (obj == null) continue;

            // Calculate distance to tornado center
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            // If within range, move object toward tornado
            if (distance <= suckRange)
            {
                Vector3 direction = (transform.position - obj.transform.position).normalized;
                obj.transform.position += direction * suckSpeed * Time.deltaTime;
            }
        }
    }
}

