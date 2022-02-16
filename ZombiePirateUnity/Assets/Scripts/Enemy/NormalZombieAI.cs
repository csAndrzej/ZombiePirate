using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieAI : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float AttackDelay;
    [SerializeField] private int AttackDamage;
    [SerializeField] private Transform Target;
    private PlayerController2D mPlayerController;
    private SpriteRenderer mSpriteRenderer;
    private DamageController mDamageController;
    private Rigidbody2D mRigidBody;
    private float LastAttackDt;
    private Vector2 Velocity;

    public static int NumberOfZombies;
    public GameObject walls;

    Color baseColor;

    void Awake()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        mPlayerController = FindObjectOfType<PlayerController2D>();
        mSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        mDamageController = GetComponent<DamageController>();
        baseColor = mSpriteRenderer.color;
        
        LastAttackDt = 0f;

        NumberOfZombies = 12;
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

        if (DistanceToTarget < 5f)
        {
            // Check if enough time passed between attacks
            if (Time.time > LastAttackDt + AttackDelay)
            {
                AttackTarget();
                // Assigning time at which the attack occurred
                LastAttackDt = Time.time;
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

    public void TakeDamage(int damage)
    {
        Health -= damage;
        mDamageController.RunEffect(mSpriteRenderer, baseColor);
    }

    private void ItemDropOnDeath()
    {
        int randomNumber = Random.Range(0, 2);

        switch (randomNumber)
        {
            case 0:
                //nothing
                break;
            case 1:
                //nothing
                break;
            case 2:
                //nothing
                break;
        }
    }
}
