using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour
{
    private Vector3 mouseInput;
    private Vector2 movementInput;
    private Vector2 currentVelocity;
    private Rigidbody2D p_RigidBody;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float health;

    void Awake()
    {
        p_RigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mouseInput = new Vector3(Input.GetAxis("Fire1"), Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentVelocity = p_RigidBody.velocity;
    }

    private void FixedUpdate()
    {
        Vector3 MouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // This is required as the Input.mousePosition gives coordinates relative to the screen rather than our world

        float targetAngle = -Mathf.Atan2(MouseWorldPoint.x, MouseWorldPoint.y) * (180 / Mathf.PI);
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), rotationSpeed * Time.deltaTime);
        // Used spherical interpolation for 'smoother' rotation of the player 

        if (movementInput != Vector2.zero)
        {
            currentVelocity.y = Mathf.Cos(p_RigidBody.rotation * (Mathf.PI / 180)) * movementInput.y + Mathf.Sin(p_RigidBody.rotation * Mathf.PI / 180) * movementInput.x;
            currentVelocity.x = -Mathf.Sin(p_RigidBody.rotation * (Mathf.PI / 180)) * movementInput.y + Mathf.Cos(p_RigidBody.rotation * Mathf.PI / 180) * movementInput.x;

            p_RigidBody.velocity += currentVelocity * moveSpeed;
        }

        if (mouseInput != Vector3.zero)
        {
            if (mouseInput.x != 0)
            {
                
            }
        }
    }

    public void TakeDamage(float attackDamage)
    {
        Debug.Log("Called!");
        health -= attackDamage;
        StartCoroutine("CastDamageEffect");
        StopCoroutine("CastDamageEffect");
        
    }

    IEnumerator CastDamageEffect()
    {
        Debug.Log("CASTDAMAGEEFFECT");
        // Original colour of the sprite 
        Color baseColor = spriteRenderer.color;
        
        spriteRenderer.color = Color.red;

        for (float time = 0; time < 1.0f; time += Time.deltaTime / 1)
        {
            spriteRenderer.color = Color.Lerp(Color.red, baseColor, time);
            yield return null;
        }

        spriteRenderer.color = baseColor;
    }
}