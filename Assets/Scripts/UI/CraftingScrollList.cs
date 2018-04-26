using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingScrollList : MonoBehaviour
{
	
	private ItemDatabase itemDatabase;
	private Inventory inventory;
	private CraftingWindow craftingWindow;
	private RequirementScrollList requirementScrollList;

	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
	
	
	// Use this for initialization
	void Start ()
	{
		craftingWindow = GameObject.FindGameObjectWithTag("CraftingWindow").GetComponent<CraftingWindow>();
		itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
		requirementScrollList = GameObject.FindGameObjectWithTag("RequirementScrollList").GetComponent<RequirementScrollList>();

		RefreshDisplay();
	}
	
	/**
	 * Refreshes the craftables scroll list. 
	 */
	public void RefreshDisplay()
	{
		RemoveButtons();
		AddButtons();
	
		if(contentPanel.childCount <= 0 || !inventory.CraftableContains(craftingWindow.GetItemName())){
			craftingWindow.craftBtn.interactable = false;
			craftingWindow.SetItemIcon(null);
			craftingWindow.SetItemName("Select an item");
			requirementScrollList.UpdateRequirements();
		}
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
	
	public int GetChildCount()
	{
		return contentPanel.childCount;
	}
	
	
}
