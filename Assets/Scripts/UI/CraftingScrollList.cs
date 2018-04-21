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

	public void RefreshDisplay()
	{
		RemoveButtons();
		AddButtons();
		Debug.Log("refresh");
	}

	private void RemoveButtons()
	{
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			buttonObjectPool.ReturnObject(toRemove);
		}
	}
	
	private void AddButtons()
	{
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
