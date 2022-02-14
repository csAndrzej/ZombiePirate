using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        //all items are listed here e.g

        //Rope,
        //HealthPotion,
        //Apples,
        //Coin 
        Barrel,                             //TEMPORARY TEST ITEM DELETE LATER
        Crate,                              //TEMPORARY TEST ITEM DELETE LATER
        Rope,
        Pistol,
        Musket,
        HealthPotion
    }

    public ItemType itemType;               //identity of the item
    public int amount;                      //amount of each item

    public Sprite GetSprite()               
    {
        //returns Sprites to be used in the inventory UI
        switch (itemType)
        {
            default:
            case ItemType.Barrel: return ItemAssets.Instance.barrelSprite;
            case ItemType.Crate: return ItemAssets.Instance.crateSprite;
            case ItemType.Rope: return ItemAssets.Instance.ropeSprite;
            case ItemType.Pistol: return ItemAssets.Instance.pistolSprite;
            case ItemType.Musket: return ItemAssets.Instance.musketSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
        }
    }

    public bool isStackable()
    {
        //Items stackable? True = Stackable - False = Unstackable
        switch (itemType)
        {
            default:
            case ItemType.Barrel:
            case ItemType.Rope:
            case ItemType.HealthPotion:
                return true;
            case ItemType.Crate:
            case ItemType.Pistol:
            case ItemType.Musket:
                return false;
        }
    }
}
