using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private int damage;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}