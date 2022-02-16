using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerController2D : MonoBehaviour
{
    
    [SerializeField] public int MaxHealth;
    [SerializeField] public static int Health;
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
    private DamageController mDamageController;
    private StartMenu mStartMenu;
    
    private AudioController mAudioController;
    public AudioClip acPain;
    public AudioClip acShootAndReload;


    //inventory variables
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    Color baseColor;

    
    
    void Awake()
    {
        MaxHealth = 100;
        Health = MaxHealth;

        mRigidBody = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mDamageController = GetComponent<DamageController>();
        mAudioController = GameObject.FindGameObjectWithTag("AudioObject").GetComponent<AudioController>();
        baseColor = mSpriteRenderer.color;

        mStartMenu = FindObjectOfType<StartMenu>();

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
        mAudioController.PlayMusic();
        if (Health <= 0)
        {
            return;
        }

            MouseInput = new Vector3(Input.GetAxis("Fire1"), Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            // Gets the input from mouse button1 and it's xy axis
            KeyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            // Gets the input from keyboard for horizontal and vertical keys
    }

    private void FixedUpdate()
    {
        if (Health <= 0)
        {
            return;
        }

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

            mAudioController.PlaySFX(acShootAndReload);
        }
    }

    private void UseItem(Item item)
    {
        //Allows player to use item in inventory, each item has different effects
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:      //TEMPORARY DELETE LATER
                //do potion stuff
                Health += MaxHealth / 3;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.Crate:       //TEMPORARY DELETE LATER
                //do crate stuff
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Crate, amount = 1 });
                break;
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (Health <= 0)
        {
            return;
        }

        mAudioController.PlaySFX(acPain);
        Health -= damage;
        mDamageController.RunEffect(mSpriteRenderer, baseColor);
    }
}
