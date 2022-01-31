using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld; //Prefab for items in the world

    //all item sprites listed here
    public Sprite barrelSprite;
    public Sprite crateSprite;
}
