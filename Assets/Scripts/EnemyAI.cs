using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;      
    public float fleeDistance = 2f;
    public float moveSpeed = 2.5f;
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
    }
}