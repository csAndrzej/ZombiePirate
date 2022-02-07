using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieAI : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;

    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange;

    [SerializeField] private int damage;
    private float lastAttackTime;
    [SerializeField] private float attackDelay;
    [SerializeField] private float rotationSpeed = 90f;



    private Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //Check if player is within attack range
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

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
                //nothing
                break;
        }
    }
}
