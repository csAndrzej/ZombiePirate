using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    private Vector2 direction;

    private Transform target;
    [SerializeField] private float rotationSpeed = 180f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiPath.reachedDestination)
        {
                //Turn to path direction
                direction = aiPath.desiredVelocity;
                transform.right = direction;
        }
        else 
        {
            //Turn to player
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; 
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

        }
    }
}
