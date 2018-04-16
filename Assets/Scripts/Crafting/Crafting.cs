using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private ItemDatabase database;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
    }

    public void CheckCrafting()
    {
        if (inventory.InventoryContains(0))
        {
            if (inventory.GetItemQuantity(0) >= database.items[4].recipe[0])
            {
                inventory.AddItem(4);
                inventory.RemoveItem(0, database.items[4].recipe[0]);
            }
        }
    }
}
