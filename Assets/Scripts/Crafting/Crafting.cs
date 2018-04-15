using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private ItemDatabase database;
    private Inventory inventory;

    private int stein = 0;
    private bool stein2;
    private int woodid = 1;
    private bool wood;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
    }

    public void CheckCrafting()
    {
        for(int i = 0; i < database.items.Count; i++)
        {
            if (inventory.InventoryContains(i))
            {
                if(i == 0)
                {
                    Debug.Log("Har funnet STOOOOONE");
                    stein2 = true;
                }
                else if(i == 1)
                {
                    Debug.Log("Har funnet WOOOOOD");
                    wood = true;
                }
            }
           
        }

    }
}
