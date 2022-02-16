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
    private Rigidbody2D mRigidBody;
    private float LastAttackDt;
    private Vector2 Velocity;

    public static int NumberOfZombies;
    public GameObject walls;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        mPlayerController = FindObjectOfType<PlayerController2D>();
        mSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
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
