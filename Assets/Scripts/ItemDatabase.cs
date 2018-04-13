using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Add items that is needed.
 */
public class ItemDatabase : MonoBehaviour
{
	public List<Item> items = new List<Item>();

	void Start()
	{
		items.Add(new Item("Stone", 0, "Just stone", Item.ItemType.Resource, true));
		items.Add(new Item("Wood", 1, "Just wood", Item.ItemType.Resource, true));
		items.Add(new Item("Meat", 2, "Just meat", Item.ItemType.Consumable, true));

        // Adding blocks
        items.Add(new Item("Woodblock", 3, Item.ItemType.Block, true, true));
        items.Add(new Item("Stoneblock", 4, Item.ItemType.Block, true, true));
    }
}
