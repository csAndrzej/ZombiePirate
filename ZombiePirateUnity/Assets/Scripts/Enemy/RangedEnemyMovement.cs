using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyMovement : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    private Vector2 direction;

    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 90f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = aiPath.desiredVelocity;
        transform.right = direction;

        if (aiPath.reachedDestination)
        {
            //////turn to the player
            //Vector3 targetDir = target.position - transform.position;
            //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; //-90f
            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; //-90f
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);
        }
    }
}
