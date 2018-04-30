using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Meat", 0, "Just meat", Item.ItemType.Consumable, true)); //TODO: 
        items.Add(new Item("Stone", 1, "Just stone", Item.ItemType.Resource, true));
        items.Add(new Item("Wood", 2, "Just wood", Item.ItemType.Resource, true));

        //Blocks
        items.Add(new Item("Woodblock", 3, Item.ItemType.Block, true, true));
        items.Add(new Item("Stoneblock", 4, Item.ItemType.Block, true, true));
        
        //Tools and Weapons
        items.Add(new Item("Sword", 5, "swerd", Item.ItemType.Weapon, false, attackdmg: 5));
        items.Add(new Item("Pickaxe", 6, "hakke", Item.ItemType.Tool, false, gatherdmg: 5));
        items.Add(new Item("Axe", 7, "øks", Item.ItemType.Tool, false, gatherdmg: 5));

    }

    public Item FetchItemByID(int id)
    {
        return items[id];
    }

    public void AddItemToDatabase(string itemName, int id, string desc, Item.ItemType type , bool stackable, int attackdmg = 1, int gatherdmg = 1)
    {
        items.Add(new Item(itemName, id, desc, type, stackable, attackdmg, gatherdmg));
    }
    
    public void AddItemToDatabase(string myName, int id, Item.ItemType type, bool amISolid, bool stackable, int attackdmg = 1, int gatherdmg = 1)
    {
        items.Add(new Item(myName, id, type, amISolid, stackable, attackdmg, gatherdmg));
    }
}