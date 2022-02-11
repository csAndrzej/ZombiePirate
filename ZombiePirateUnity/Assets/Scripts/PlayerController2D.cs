using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerController2D : MonoBehaviour
{
   
    [SerializeField] private int MaxHealth;
    [SerializeField] private int Health;
    [SerializeField] private int MeleeDamage;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float AttackDelay;

    [SerializeField] private GameObject Bullet;
    private float LastAttackDt;

    private Vector3 MouseInput;
    private Vector2 KeyboardInput;
    private Vector2 Velocity;
    private Rigidbody2D mRigidBody;
    private SpriteRenderer mSpriteRenderer;

    //inventory variables
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    
    
    void Awake()
    {
        MaxHealth = 100;
        MaxHealth = Health;

        mRigidBody = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this.gameObject);
        uiInventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(20, 0), new Item { itemType = Item.ItemType.Rope, amount = 2 });    //TEST FUNCTION DELETE LATER
        //ItemWorld.SpawnItemWorld(new Vector3(-20, 0), new Item { itemType = Item.ItemType.Crate, amount = 1 });    //TEST FUNCTION DELETE LATER
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //This happens when player collides with item in world
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null) //add item to inventory
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    
    void Update()
    {
        
        MouseInput = new Vector3(Input.GetAxis("Fire1"), Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // Gets the input from mouse button1 and it's xy axis
        KeyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Gets the input from keyboard for horizontal and vertical keys
    }

    private void FixedUpdate()
    {
        Vector3 MouseCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);   // Get mouse coordinates relative to our screen

        float Theta = -Mathf.Atan2(MouseCoord.x - transform.position.x, MouseCoord.y - transform.position.y) * (180 / Mathf.PI);
        // The angle used to orient the player based on where the mouse is

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, Theta), RotationSpeed * Time.deltaTime);
        // Used spherical interpolation for 'smoother' rotation of the player 

        if (KeyboardInput != Vector2.zero)
        {
            Velocity.y = Mathf.Cos(mRigidBody.rotation * (Mathf.PI / 180)) * KeyboardInput.y + Mathf.Sin(mRigidBody.rotation * Mathf.PI / 180) * KeyboardInput.x;
            Velocity.x = -Mathf.Sin(mRigidBody.rotation * (Mathf.PI / 180)) * KeyboardInput.y + Mathf.Cos(mRigidBody.rotation * Mathf.PI / 180) * KeyboardInput.x;
            // Using trigonometry to work out scalar direction of the player's movement
            // Instead of just forward input, it takes into account A, D input to allow the player
            // to move sideways while moving forward to create smoother controls
            mRigidBody.velocity += Velocity * MovementSpeed;
        }

        if (MouseInput.x == 1 && Time.time > LastAttackDt + AttackDelay)
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            LastAttackDt = Time.time;
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
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
    
    public void TakeDamage(int damage)
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
}
