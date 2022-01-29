using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector2 movementInput;
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
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
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
    }
}