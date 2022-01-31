using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector2 movementInput;
    private Vector2 currentVelocity;
    private Rigidbody2D p_RigidBody;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    //inventory variables
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    void Awake()
    {
        p_RigidBody = GetComponent<Rigidbody2D>();

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this.gameObject);
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(20, 0), new Item { itemType = Item.ItemType.Barrel, amount = 1 });    //TEST FUNCTION DELETE LATER
        ItemWorld.SpawnItemWorld(new Vector3(-20, 0), new Item { itemType = Item.ItemType.Crate, amount = 1 });    //TEST FUNCTION DELETE LATER
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //This happens when player collides with item in world
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null) //add item to inventory
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
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
    
    private void UseItem(Item item)
    {
        //Allows player to use item in inventory, each item has different effects
        switch (item.itemType)
        {
            case Item.ItemType.Barrel:      //TEMPORARY DELETE LATER
                //do barrel stuff
                Destroy(this.gameObject);
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Barrel, amount = 1 });
                break;
            case Item.ItemType.Crate:       //TEMPORARY DELETE LATER
                //do crate stuff
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Crate, amount = 1 });
                break;
        }
    }
}