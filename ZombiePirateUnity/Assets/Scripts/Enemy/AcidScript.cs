using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidScript : MonoBehaviour
{
    [SerializeField] private float timeUntilGone = 3.5f;
    [SerializeField] private float AttackDelay;
    [SerializeField] private int AttackDamage;
    private float LastAttackDt;
    private PlayerController2D mPlayerController;

    void Start()
    {
        mPlayerController = FindObjectOfType<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilGone < 0)
            Destroy(gameObject);

        timeUntilGone -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // Check if enough time passed between attacks
            if (Time.time > LastAttackDt + AttackDelay)
            {
                AttackTarget();
                // Assigning time at which the attack occurred
                LastAttackDt = Time.time;
            }
        }
    }

    void AttackTarget()
    {
        mPlayerController.TakeDamage(AttackDamage);
    }
}
