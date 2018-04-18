using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Meat", 0, "Just meat", Item.ItemType.Consumable, true, CreateRecipe(0, 0))); //TODO: 
        items.Add(new Item("Stone", 1, "Just stone", Item.ItemType.Resource, true, CreateRecipe(0, 0)));
        items.Add(new Item("Wood", 2, "Just wood", Item.ItemType.Resource, true, CreateRecipe(0, 0)));

        // Adding blocks

        items.Add(new Item("Woodblock", 3, Item.ItemType.Block, true, true, CreateRecipe(2, 2)));
        items.Add(new Item("Stoneblock", 4, Item.ItemType.Block, true, true, CreateRecipe(1, 2)));
    }

    List<int> CreateRecipe(int id1, int amount1, int id2 = 0, int amount2 = 0)
    {
        List<int> recipe = new List<int> {id1, amount1, id2, amount2};

        return recipe;
    }

    public Item FetchItemByID(int id)
    {
        return items[id];
    }
}