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
	private bool showTooltip;
	private string tooltip;
	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;
	private int iconSize;
	
	
	// Use this for initialization
	void Start ()
	{
		iconSize = 30;
		
		// Fill the slots list with empty items.
		for (int i = 0; i < slotsX*slotsY; i++)
		{
			slots.Add(new Item());
			inventory.Add(new Item());
		}
		
		database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		
		// Adding items to empty inventory slots
		AddItem(0);
		AddItem(1);
				
	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
		}
		
		if (draggingItem) {
			inventory[prevIndex] = draggedItem;
			draggingItem = false;
			draggedItem = null;
		}
	}	
	
	//Unity method to draw in screen space
	void OnGUI ()
	{
		tooltip = "";
		GUI.skin = skin;
		
		if (showInventory)
		{	
			DrawInventory();	
		}
		
		if(showTooltip && showInventory)
		{
			GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 150, 150), tooltip, skin.GetStyle("Tooltip"));
		}

		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, iconSize, iconSize), draggedItem.itemIcon );
		}

		showTooltip = false;
	}

	void DrawInventory()
	{	
		Event e = Event.current;;
		int i = 0;
		
		//Draws slots
		for (int y = 0; y < slotsY; y++)	
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect(x * iconSize, y * iconSize, iconSize, iconSize);
				GUI.Box(new Rect(x * iconSize, y * iconSize, iconSize, iconSize), "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];

				if (slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					
					if (slotRect.Contains(e.mousePosition))
					{
						tooltip = "<color=#ffffff><b>" + slots[i].itemName + " </b> \n\n" +  slots[i].itemDesc + "</color>";
						showTooltip = true;
						
						//Drag item
						if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
						}

						//Swap items position
						if (e.type == EventType.MouseUp && draggingItem)
						{
							inventory[prevIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				else
				{	// Allows to drag an item to an empty slot
					if (slotRect.Contains(e.mousePosition))
					{
						if (e.type == EventType.MouseUp && draggingItem)
						{
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				
				i++;
			}
		}
	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}

	void AddItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemName == null)
			{
				for (int j = 0; j < database.items.Count; j++)
				{
					if (database.items[j].itemID == id)
					{
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		foreach(Item item in inventory){
			if(item.itemID == id){ 
				return true; 
			}
		}
		return false; 
	}
}
