using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public int itemID;
    public string itemName;
    public string itemDesc;
    public Texture2D itemIcon;
    public ItemType itemType;
    public bool isStackable;
    public int itemQuantity;
    public int attackDamage;
    public int gatherDamage;
    public bool isSolid;
    public List<int> recipe;

    //Itemtypes eg. weapon, consumable, tools etc.
    public enum ItemType
    {
        Resource,
        Block,
        Consumable,
        Tool,
        Weapon,
        Armor //Headgear, chest, leggings?
    }

    /**
     * Item constructor when an empty item is needed.
     * */
    public Item()
    {
        itemID = -1;
        attackDamage = 1;
        gatherDamage = 1;
    }

    /**
     * Constructor for Items, Give the item a name, id, description. The Item type of your item can be found in the item.cs enum. Add a new type if needed. 
     * bool stackable is used to make the item(s) stack in inventory, the bool amISolid is used for when the item(s) is placed on the ground, solid blocks stops the player.
     * The attack dmg and gatherdmg is used for the item(s) damage on enemies and when gathering resources.
     **/
    public Item(string name, int id, string desc, ItemType type, bool stackable, bool amISolid, int attackdmg = 1, int gatherdmg = 1)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemType = type;
        isStackable = stackable;
        isSolid = amISolid;
        attackDamage = attackdmg;
        gatherDamage = gatherdmg;

        itemIcon = Resources.Load<Texture2D>("ItemIcons/" + name);
        itemQuantity = 1; //TODO: Tell heller inventory for matchende ID. 
    }
}