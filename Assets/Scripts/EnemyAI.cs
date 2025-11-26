using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;      
    public float fleeDistance = 2f;
    public float moveSpeed = 2.5f;
    
    [Header("Shooting Settings")]
    [Tooltip("Prefab of the bullet to shoot")]
    public GameObject bulletPrefab;
    
    [Tooltip("Time between shots (in seconds)")]
    public float shootInterval = 2f;
    
    [Tooltip("Minimum distance to start shooting")]
    public float shootRange = 15f;
    
    private float lastShotTime = 0f;
    
    void Start()
    {
    if (target == null) //asssigns the enemies target to the tornado at runtime
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null)
            target = playerObj.transform;
        }
    }
    void Update()
    {
        if (target == null) return;

        Vector3 toPlayer = target.position - transform.position;
        toPlayer.y = 0f;

        float distance = toPlayer.magnitude;

        Vector3 moveDir;

        if (distance > fleeDistance)
        {
            //if enemy is far it will start moving towards the player
            moveDir = toPlayer.normalized;
        }
        else
        {
            //if the enemy is too close it will move away from the player
            moveDir = (-toPlayer).normalized;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (moveDir != Vector3.zero) //rotates the enemy so when we add a model later it looks chill
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        
        // Shooting logic
        if (bulletPrefab != null && distance <= shootRange)
        {
            // Look at the target for shooting
            Vector3 lookDirection = toPlayer.normalized;
            if (lookDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = lookRotation;
            }
            
            // Shoot if enough time has passed
            if (Time.time - lastShotTime >= shootInterval)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab == null || target == null) return;
        
        // Calculate direction to target
        Vector3 direction = (target.position - transform.position).normalized;
        
        // Spawn bullet slightly in front of enemy to avoid immediate collision
        Vector3 spawnPosition = transform.position + direction * 0.5f;
        
        // Instantiate bullet facing the target
        Quaternion bulletRotation = Quaternion.LookRotation(direction);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, bulletRotation);
        
        // Tell the bullet which enemy shot it so it can ignore collisions
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetShooter(gameObject);
        }
    }
}