using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Vector2 moveDirection;
    private Vector2 currentVelocity;
    private Rigidbody2D p_RigidBody;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    void Awake()
    {
        p_RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentVelocity = p_RigidBody.velocity;
    }

    private void FixedUpdate()
    {
        if (moveDirection.x != 0)
        {
            p_RigidBody.rotation -= moveDirection.x * rotationSpeed;
        }
        if (moveDirection.y != 0)
        {
            currentVelocity.y = Mathf.Cos(p_RigidBody.rotation * (Mathf.PI / 180)) * moveDirection.y;
            currentVelocity.x = -Mathf.Sin(p_RigidBody.rotation * (Mathf.PI / 180)) * moveDirection.y;

            p_RigidBody.velocity += currentVelocity * moveSpeed;
        }
    }
    

}