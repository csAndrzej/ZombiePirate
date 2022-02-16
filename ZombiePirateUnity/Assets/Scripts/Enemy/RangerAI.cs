using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangerAI : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    Transform currentPartolPoint;
    int currentPatrolIndex;

    [SerializeField] private Transform target;
    [SerializeField] public float chaseRange;
    [SerializeField] private float stopChaseRange;

    [SerializeField] private float attackRange;
    public int damage;
    private float lastAttackTime;
    [SerializeField] private float attackDelay;
    private float rotationSpeed = 360f;

    [SerializeField] private float distanceToBackAway;

    public GameObject projectile;
    public float projectileForce;

    private Rigidbody2D rb;

    private AIPath aiPath;
    private SpriteRenderer mSpriteRenderer;
    private DamageController mDamageController;

    private Color baseColor;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mDamageController = GetComponent<DamageController>();
        baseColor = mSpriteRenderer.color;


        target = GameObject.Find("Player").transform;
    }

    /*Code Version 1

    void Update()
    {
        //Check if player is within attack range
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //Raycast to check line of sight to target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position);//, attackRange);

        if (distanceToPlayer < chaseRange)
        {
            //turn to the player
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; //-90f
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            if (distanceToPlayer > stopChaseRange)
            {
                //move to the player
                Vector3 direction = target.position - transform.position;
                rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
            }
        }


        if (distanceToPlayer < distanceToBackAway)
        {
            //move away from the player
            Vector3 direction = transform.position - target.position;
            rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        }

        if (distanceToPlayer < attackRange)
        {
            //Check attack delay
            if (Time.time > lastAttackTime + attackDelay)
            {
                //Hit anything?
                //if (hit.transform == target)
                //{
                //hit player = fire projectile
                GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
                newProjectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileForce, 0f));
                lastAttackTime = Time.time;
                //}
            }
        }

        //On death
        if (health <= 0)
        {
            ItemDropOnDeath();

            Destroy(gameObject); //Method of death
        }
    }

    private void ItemDropOnDeath()
    {
        int randomNumber = Random.Range(0, 2);

        switch (randomNumber)
        {
            case 0:
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 1 });
                break;
            case 1:
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 2 });
                break;
            case 2:
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Musket, amount = 1 });
                break;
        }
    }
    */

    void Update()
    {
        if (aiPath.reachedDestination)
        {
            //////turn to the player
            //Vector3 targetDir = target.position - transform.position;
            //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; //-90f
            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            //Check attack delay
            if (Time.time > lastAttackTime + attackDelay)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
                newProjectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileForce, 0f));
                lastAttackTime = Time.time;
            }
        }

        // On death
        if (health <= 0)
        {
            //ItemDropOnDeath();
            Destroy(gameObject); // Method of death
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().Damage);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        mDamageController.RunEffect(mSpriteRenderer, baseColor);
    }
}