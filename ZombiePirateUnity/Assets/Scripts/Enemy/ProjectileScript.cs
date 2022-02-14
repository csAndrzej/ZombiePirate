using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private int AttackDamage;
    private PlayerController2D mPlayerController;
    [SerializeField] private GameObject acid;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mPlayerController = FindObjectOfType<PlayerController2D>();
    }

    private void Update()
    {
        //if velocity < 0 destroy object
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Collision with player
        if (collider.CompareTag("Player"))
        {
            //reduce health
            AttackTarget();
            Vector3 pos = transform.position;
            GameObject acidSplash = Instantiate(acid,pos,Quaternion.identity);

            Destroy(gameObject);
        }

        //If collision occurs with anything but enemies
        if (!collider.CompareTag("Enemy"))
        {
            Vector3 pos = transform.position;
            GameObject acidSplash = Instantiate(acid, pos, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void AttackTarget()
    {
        mPlayerController.TakeDamage(AttackDamage);
    }
}