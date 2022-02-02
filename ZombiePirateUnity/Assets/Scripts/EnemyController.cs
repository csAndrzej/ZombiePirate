using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float health;
    [SerializeField] private float attackInterval;
    [SerializeField] private float attackDamage;
    [SerializeField] private BoxCollider2D range;

    private Vector2 currentVelocity;
    private Rigidbody2D p_RigidBody;
    private Transform target;
    private GameObject player;
    private PlayerController2D playerController;
    private bool isPlayerInRange;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController2D>();
        p_RigidBody = GetComponent<Rigidbody2D>();
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange == true)
        {
            if (Vector3.Distance(transform.position, target.position) > 5f)
            {
                currentVelocity = p_RigidBody.velocity;
                GoToPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            RemainIdle();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInRange = false;
        }
    }

    private void GoToPlayer()
    {
        Vector2 Pos = new Vector2(target.position.x, target.position.y);
        float targetAngle = -Mathf.Atan2(Pos.x - transform.position.x, Pos.y - transform.position.y) * (180 / Mathf.PI);
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), rotationSpeed * Time.deltaTime);

        currentVelocity.y = Mathf.Cos(p_RigidBody.rotation * (Mathf.PI / 180));
        currentVelocity.x = -Mathf.Sin(p_RigidBody.rotation * (Mathf.PI / 180));

        p_RigidBody.velocity += currentVelocity * moveSpeed;
    
    }

    private void AttackPlayer()
    {
        playerController.TakeDamage(attackDamage);
    }

    private void RemainIdle()
    {
        Debug.Log("Remaining idle");
    }
}
