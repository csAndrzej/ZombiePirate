using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieAI : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float Range;
    [SerializeField] private float AttackDelay;
    [SerializeField] private int AttackDamage;
    [SerializeField] private Transform Target;
    private PlayerController2D mPlayerController;
    private SpriteRenderer mSpriteRenderer;
    private Rigidbody2D mRigidBody;
    private float LastAttackDt;
    private Vector2 Velocity;

    public int NumberOfZombies;
    public GameObject walls;
    
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        mPlayerController = FindObjectOfType<PlayerController2D>();
        LastAttackDt = 0f;

        NumberOfZombies = 2;
    }
    public void Update()
    {
        if (NumberOfZombies <= 0)
        {
            Destroy(walls);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NumberOfZombies--;
        }
    }
    void FixedUpdate()
    {
        // Check if target is withing the range to follow it 
        float DistanceToTarget = Vector3.Distance(transform.position, Target.position);

        if (DistanceToTarget < Range)
        {
            if (DistanceToTarget > 5f)
            {
                FollowTarget();
            }
            else 
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
        /*
        else 
        {
         Function that corresponds to some idle action, e.g pacing around  
        }
        */

        // On death
        if (Health <= 0)
        {
            ItemDropOnDeath();
            Destroy(gameObject); // Method of death

            NumberOfZombies--;
        }
    }

    void FollowTarget()
    {
        Vector3 DistanceDiff = Target.position - transform.position;
        float Theta = -Mathf.Atan2(Target.position.x - transform.position.x, Target.position.y - transform.position.y) * (180 / Mathf.PI);
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, Theta), RotationSpeed * Time.deltaTime);
        Velocity.y = Mathf.Cos(mRigidBody.rotation * (Mathf.PI / 180));
        Velocity.x = -Mathf.Sin(mRigidBody.rotation * (Mathf.PI / 180));

        mRigidBody.velocity += Velocity * MovementSpeed;
    }

    void AttackTarget()
    {
        mPlayerController.TakeDamage(AttackDamage);
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
        Health -= damage;
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
