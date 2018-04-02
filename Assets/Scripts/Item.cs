using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public string itemDesc;
    public Texture itemIcon;
    public ItemType itemType;
    public bool isStackable;
    public int itemQuantity;

    //Itemtypes eg. weapon, consumable, tools etc.
    public enum ItemType
    {
        Resource, 
        Consumable,
        Tool,
        Weapon,
        Armor //Headgear, chest, leggings?
    }

    public Item()
    {
        itemID = -1;
    }
    
    public Item(string name, int id, string desc, ItemType type, bool stackable)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemType = type;
        itemIcon = Resources.Load<Texture2D>("ItemIcons/" + name);
        isStackable = stackable;
        itemQuantity = 1;
    }

}
