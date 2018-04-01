using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public int slotsX, slotsY;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	public GUISkin skin;
	
	private ItemDatabase database;
	private bool showInventory;
	
	// Use this for initialization
	void Start ()
	{	
		// Fill the slots list with empty items.
		for (int i = 0; i < (slotsX*slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add(new Item());
		}
		
		database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		inventory[0] = database.items[0];
		inventory[1] = database.items[1];

	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
		}
	}	
	
	//Unity method to draw in screen space
	void OnGUI ()
	{
		GUI.skin = skin;
		if (showInventory)
		{	
			DrawInventory();	
		}
		
	}

	void DrawInventory()
	{
		//Index value 
		int i = 0;
		
		//Draws slots
		for (int y = 0; y < slotsY; y++)	
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect(x * 50, y * 50, 50, 50);
				GUI.Box(new Rect(x * 50, y * 50, 50, 50), "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];

				if (slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
				}
				
				i++;
			}
		}
	}
}
