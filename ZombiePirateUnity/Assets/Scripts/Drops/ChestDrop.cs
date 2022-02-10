using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int randomNumber = Random.Range(0, 2);

            switch (randomNumber)
            {
                case 0:
                    ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Musket, amount = 1 });
                    break;
                case 1:
                    ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Pistol, amount = 1 });
                    break;
                case 2:
                    ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Pistol, amount = 1 });
                    break;
            }

            Destroy(gameObject);
        }
    }
}
