using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
	public List<Item> items = new List<Item>();

	void Start()
	{
		items.Add(new Item("Stone", 0, "Just stone", Item.ItemType.Resource));
		items.Add(new Item("Wood", 1, "Just wood", Item.ItemType.Resource));

	}
}
