using UnityEngine;
using System.Collections.Generic;

// Pulls in objects with a specific tag toward the tornado
public class TornadoSuction : MonoBehaviour
{
    [Header("Suction Settings")]
    [Tooltip("Distance at which objects start being sucked in")]
    public float suckRange = 1f;
    
    [Tooltip("How often to find suckable objects (in seconds). Lower = more accurate but more expensive")]
    public float findInterval = 0.2f;

    [Header("Small Objects")]
    [Tooltip("Tag for small objects")]
    public string smallTag = "SuckableSmall";
    [Tooltip("Speed for small objects")]
    public float smallSpeed = 10f;

    [Header("Medium Objects")]
    [Tooltip("Tag for medium objects")]
    public string mediumTag = "SuckableMedium";
    [Tooltip("Speed for medium objects")]
    public float mediumSpeed = 5f;

    [Header("Large Objects")]
    [Tooltip("Tag for large objects")]
    public string largeTag = "SuckableLarge";
    [Tooltip("Speed for large objects")]
    public float largeSpeed = 2f;

    private GameObject[] smallObjects;
    private GameObject[] mediumObjects;
    private GameObject[] largeObjects;
    private float lastFindTime = 0f;

    void Update()
    {
        // Periodically find all suckable objects
        if (Time.time - lastFindTime >= findInterval)
        {
            lastFindTime = Time.time;
            smallObjects = GameObject.FindGameObjectsWithTag(smallTag);
            mediumObjects = GameObject.FindGameObjectsWithTag(mediumTag);
            largeObjects = GameObject.FindGameObjectsWithTag(largeTag);
        }

        // Move objects every frame for smooth movement
        SuckInObjects(smallObjects, smallSpeed);
        SuckInObjects(mediumObjects, mediumSpeed);
        SuckInObjects(largeObjects, largeSpeed);
    }

    void SuckInObjects(GameObject[] objects, float speed)
    {
        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;

            // Calculate distance to tornado center
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            // If within range, move object toward tornado
            if (distance <= suckRange)
            {
                Vector3 direction = (transform.position - obj.transform.position).normalized;
                obj.transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}