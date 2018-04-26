using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Meat", 0, "Just meat", Item.ItemType.Consumable, true, false));
        items.Add(new Item("Stone", 1, "Just stone", Item.ItemType.Resource, true, false));
        items.Add(new Item("Wood", 2, "Just wood", Item.ItemType.Resource, true, false));

        //Blocks
        items.Add(new Item("Woodblock", 3, "Woodblock for building", Item.ItemType.Block, true, true));
        items.Add(new Item("Stoneblock", 4, "Stoneblock for building", Item.ItemType.Block, true, true));
        
        //Tools and Weapons
        items.Add(new Item("Sword", 5, "swerd", Item.ItemType.Weapon, false, false, attackdmg: 5));
        items.Add(new Item("Pickaxe", 6, "hakke", Item.ItemType.Tool, false, false, gatherdmg: 5));
        items.Add(new Item("Axe", 7, "øks", Item.ItemType.Tool, false, false, gatherdmg: 5));

    }

    // Gets an item by id. 
    public Item GetItem(int id)
    {
        return items[id];
    }
}