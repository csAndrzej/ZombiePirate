using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MotherZombieAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private int health;
    [SerializeField] private GameObject[] spawnableZombies;

    [SerializeField] private float attackRange;
    [SerializeField] private float spawnRange;
    private float lastAttackTime;
    [SerializeField] private float attackDelay;
    [SerializeField] private float rotationSpeed = 45f;
    private SpriteRenderer mSpriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        //Check if player is within spawn range
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < attackRange)
        {
            //turn to the player
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            //Check attack delay
            if (Time.time > lastAttackTime + attackDelay)
            {
                //spawn zombies with timer
                GameObject newZombie = Instantiate(spawnableZombies[Random.Range(0, spawnableZombies.Length)], transform.position, transform.rotation);
                newZombie.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newZombie.transform.position = new Vector2(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y + Random.Range(-spawnRange, spawnRange));
                newZombie.transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = 5;
                newZombie.GetComponent<AIDestinationSetter>().target = target;

                lastAttackTime = Time.time;
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
                //ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 1 });
                break;
            case 1:
                //ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Rope, amount = 2 });
                break;
            case 2:
                //ItemWorld.SpawnItemWorld(gameObject.transform.position, new Item { itemType = Item.ItemType.Pistol, amount = 1 });
                break;
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
        StartCoroutine("CastDamageEffect");
    }
    IEnumerator CastDamageEffect()
    {
        // Original colour of the sprite 
        Color baseColor = mSpriteRenderer.color;

        mSpriteRenderer.color = Color.red;

        for (float time = 0; time < 1.0f; time += Time.deltaTime / 1)
        {
            mSpriteRenderer.color = Color.Lerp(Color.red, baseColor, time);
            yield return null;
        }

        mSpriteRenderer.color = baseColor;
    }
}