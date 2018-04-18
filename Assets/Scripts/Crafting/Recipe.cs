using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public string itemName;
    public Dictionary<string, int> items;

    public Recipe(string name, Dictionary<string, int> materials)
    {
        itemName = name;
        items = materials;
    }
}
