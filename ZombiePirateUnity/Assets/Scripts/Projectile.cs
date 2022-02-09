using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int MovementSpeed;
    [SerializeField] public int Damage;

    void Update()
    {
        transform.Translate( (Mathf.Sin(transform.rotation.z * (Mathf.PI / 180)) * MovementSpeed * Time.deltaTime),
                             (Mathf.Cos(transform.rotation.z * (Mathf.PI / 180)) * MovementSpeed * Time.deltaTime), 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
