using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float chaseRange = 10f;
    private Transform target;
    private AIPath aiPath;

    private RangerAI rangerAI;
    private SuperZombie superZombie;
    private bool superZombieBool = false;

    [HideInInspector] public bool aggro = false;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        aiPath = GetComponent<AIPath>();

        if (GetComponent<RangerAI>())
        {
            rangerAI = GetComponent<RangerAI>();
            chaseRange = rangerAI.chaseRange;
        }
        else if (GetComponent<SuperZombie>())
        {
            superZombie = GetComponent<SuperZombie>();
            chaseRange = superZombie.chaseRange;
            superZombieBool = true;
        }
    }

  
    void Update()
    {
        // Check if target is withing the range to follow it 
        float DistanceToTarget = Vector3.Distance(transform.position, target.position);

        
        //if (chaseRange < DistanceToTarget)
        //    aiPath.enabled = false;
        //else
        //    aiPath.enabled = true;

        if (superZombieBool)
        {
            if (superZombie.transformed)
            {
                if (chaseRange >= DistanceToTarget || aggro == true)
                    aiPath.enabled = true;
                else
                    aiPath.enabled = false;
            }
        }
        else
        {
            if (chaseRange >= DistanceToTarget || aggro == true)
                aiPath.enabled = true;
            else
                aiPath.enabled = false;
        }
    }
}
