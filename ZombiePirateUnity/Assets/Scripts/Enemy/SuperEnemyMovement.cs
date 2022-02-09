using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


//SPECIFIC SCRIPT FOR MOVEMENT OF SUPERZOMBIE SPRITE
public class SuperEnemyMovement : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    private Vector2 direction;

    private Transform target;
    private bool transformed;
    private bool startedRunning;
    [SerializeField] private float rotationSpeed = 180f;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //Check if zombie transformed
        if (!transformed)
        {
            startedRunning = transform.parent.GetComponent<SuperZombie>().startRunning;
            transformed = transform.parent.GetComponent<SuperZombie>().transformed;
        }


        if (!aiPath.reachedDestination && transformed)
        {
            direction = aiPath.desiredVelocity;
            transform.right = direction;
        }
        else if (transformed)
        {
            //Turn to player
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);
        }
        else if(startedRunning)
        {
            //turn away from the player
            Vector3 targetDir = transform.position - target.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);
        }

    }
}
