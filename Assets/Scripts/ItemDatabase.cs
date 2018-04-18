using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Meat", 0, "Just meat", Item.ItemType.Consumable, true));
        items.Add(new Item("Stone", 1, "Just stone", Item.ItemType.Resource, true));
        items.Add(new Item("Wood", 2, "Just wood", Item.ItemType.Resource, true));

        // Adding blocks
        items.Add(new Item("Woodblock", 3, Item.ItemType.Block, true, true));
        items.Add(new Item("Stoneblock", 4, Item.ItemType.Block, true, true));
    }
}