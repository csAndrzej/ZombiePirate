using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private GameObject player; 

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;                        //number of columns in inventory
        int y = 0;                        //number of rows in inventory
        float itemSlotCellSize = 80f;     //offset of each item in inventory UI

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransfrom = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransfrom.gameObject.SetActive(true);

            //Right click to use item
            itemSlotRectTransfrom.GetComponent<Button_UI>().ClickFunc = () => 
            {
                inventory.UseItem(item);
            };

            //Left click to drop item
            itemSlotRectTransfrom.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position, duplicateItem);
            };

            itemSlotRectTransfrom.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransfrom.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text uiText = itemSlotRectTransfrom.Find("text").GetComponent<Text>();
            if (item.amount > 1)        //Display amount if player has >1 stackable items
            {
                uiText.text = item.amount.ToString();
            }
            else uiText.text = "";      //Else don't display number
            x++;

        }
    }
}
