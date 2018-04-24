using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingScrollList : MonoBehaviour
{
	
	private ItemDatabase itemDatabase;
	private Inventory inventory;

	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
	
	// Use this for initialization
	void Start ()
	{
		itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
		RefreshDisplay();
	}
	
	/**
	 * Refreshes the craftables scroll list. 
	 */
	public void RefreshDisplay()
	{
		RemoveButtons();
		AddButtons();
	}

	/**
	 * Removes all child members of the contenPanel in craftingScrollList
	 */
	private void RemoveButtons()
	{
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			buttonObjectPool.ReturnObject(toRemove);
		}
	}
	
	/**
	 * Add button for every craftable items available.
	 */
	private void AddButtons()
	{
		//Iterating through to get the craftable item's values for making the button.
		for (int i = 0; i < inventory.craftable.Count; i++)
		{
			for (int j = 0; j < itemDatabase.items.Count; j++)
			{
				if (inventory.craftable[i] == itemDatabase.items[j].itemName)
				{
					Item craftableItem = itemDatabase.items[j];
					GameObject newButton = buttonObjectPool.GetObject();
					newButton.transform.SetParent(contentPanel, false);
					
					CraftableItemBtn craftableItemBtn = newButton.GetComponent<CraftableItemBtn>();
					craftableItemBtn.Setup(craftableItem);
				}
			}
		}
	}
	
}
