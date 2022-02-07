using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperZombie : MonoBehaviour
{
    [SerializeField] private float transformationSize;
    [SerializeField] private int health;
    [SerializeField] private float speed;

    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange;

    [SerializeField] private int damage;
    private float lastAttackTime;
    [SerializeField] private float attackDelay;
    [SerializeField] private float rotationSpeed = 90f;

    [SerializeField] private float distanceToBackAway;

    private bool transformed = false;
    [SerializeField] private float transformationTimer;
    private bool startRunning = false;

    private Item[] itemDrops;

    private Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        //Item Drops for zombie
        //itemDrops[0] = new Item { itemType = Item.ItemType.Rope, amount = 3 };
        //itemDrops[1] = new Item { itemType = Item.ItemType.Rope, amount = 4 };
        //itemDrops[2] = new Item { itemType = Item.ItemType.Rope, amount = 5 };
    }

    void Update()
    {
        //Check if player is within attack range
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (transformed)
        {
            if (distanceToPlayer < chaseRange)
            {
                //turn to the player
                Vector3 targetDir = target.position - transform.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; //-90f
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

                //move to the player
                Vector3 direction = target.position - transform.position;
                rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));

                //Check attack delay
                if (Time.time > lastAttackTime + attackDelay)
                {
                    //damage player
                }
            }
        }
        else
        {
            if (startRunning)
            {
                //Timer to transform
                transformationTimer -= Time.deltaTime;

                //turn away from the player
                Vector3 targetDir = transform.position - target.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

                //move away from the player
                Vector3 direction = transform.position - target.position;
                rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));

                if (transformationTimer <= 0f)
                    Transformation();
            }
            else if (distanceToPlayer < distanceToBackAway)
                startRunning = true;
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
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 3 });
                break;
            case 1:
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 4 });
                break;
            case 2:
                ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 5 });
                break;
        }
    }

    void Transformation()
    {
        transformed = true;

        //Variables that change during transformation
        health *= 10;
        speed /= 2;
        transform.localScale = new Vector3(transform.localScale.x * transformationSize, transform.localScale.y * transformationSize, transform.localScale.z);
    }
}
