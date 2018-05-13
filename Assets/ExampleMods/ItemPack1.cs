using UnityEngine;
using System.IO;
public class ItemPack1 : MonoBehaviour
{

	public static void InstantiateMe()
	{
		var database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		var inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

		//Add the item to database
		database.AddItemToDatabase("Helmet", 8, "New item added by mod", Item.ItemType.Armor, false);

		//Add the item to inventory
		inventory.AddItem(8); 
	}
	
}
