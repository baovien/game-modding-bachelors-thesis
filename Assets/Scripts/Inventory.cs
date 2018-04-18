using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;
    public List<string> craftable = new List<string>();

    private PlayerHealthManager playerHealthManager;
    private ItemDatabase database;
    private RecipeDatabase recipeDatabase;

    private bool showInventory;
    private bool showTooltip;
    private string tooltip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;
    private int iconSize;
    private bool isInventoryOpen;

    // Use this for initialization
    void Start()
    {
        playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        recipeDatabase = GameObject.FindGameObjectWithTag("RecipeDatabase").GetComponent<RecipeDatabase>();
        iconSize = 40;

        // Fill the slots list with empty items.
        for (int i = 0; i < slotsX * slotsY; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        AddItem(1);
        AddItem(0);
    }

    void Update()
    {
        // a crafting test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Craft("Stoneblock");
        }

        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
            isInventoryOpen = !isInventoryOpen;

            // THIS SHOULD NOT BE DONE WHEN "INVENTORY" IS CLICKED >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            for (int i = 0; i < recipeDatabase.recipes.Count; i++)
            {
                bool canCraft = true;
                foreach (var mat in recipeDatabase.recipes[i].items.Keys)
                {
                    if (!InventoryContains(mat, recipeDatabase.recipes[i].items[mat]))
                    {
                        //if one of the materials are missing the item can not be crafted, set false for that recipe.                   
                        canCraft = false;
                    }
                }
                if (canCraft)
                {
                    // If something is craftable, add it to the list containing items we can craft.
                    if (!CheckCraftable(recipeDatabase.recipes[i].itemName))
                    {
                        craftable.Add(recipeDatabase.recipes[i].itemName);
                        Debug.Log(recipeDatabase.recipes[i].itemName + " is craftable!");
                    }
                    else
                    {
                        Debug.Log("An item is craftable but is already ready in the craftable list");
                    }
                }
            }
            // THIS SHOULD NOT BE DONE WHEN "INVENTORY" IS CLICKED >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> /


            if (draggingItem)
            {
                inventory[prevIndex] = draggedItem;
                draggingItem = false;
                draggedItem = null;
            }
        }
    }

    // When the player craft the item we need to do something ...
    public void Craft(string item)
    {
        for (int i = 0; i < recipeDatabase.recipes.Count; i++)
        {
            if (recipeDatabase.recipes[i].itemName == item)
            {
                foreach (var mat in recipeDatabase.recipes[i].items.Keys)
                {
                    // mat is material, items[mat] is the amount
                    RemoveItem(mat, recipeDatabase.recipes[i].items[mat]);
                    Debug.Log(recipeDatabase.recipes[i].items[mat]);
                }
            }
        }
    }
    bool CheckCraftable(string itemName)
    {
        for (int i = 0; i < craftable.Count; i++)
        {
            if (craftable[i] == itemName)
            {
                return true;
            }
        }
        return false;
    }

    //Unity method to draw in screen space
    void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            DrawInventory();

            if (GUI.Button(new Rect(40, 450, 100, 40), "Save"))
            {
                SaveInventory();
            }

            if (GUI.Button(new Rect(40, 500, 100, 40), "Load"))
            {
                LoadInventory();
            }
        }

        //Show tooltip when hovering over an item
        if (showTooltip && showInventory)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 150, 150), tooltip,
                skin.GetStyle("Tooltip"));
        }

        //Draw item when dragged
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, iconSize, iconSize),
                draggedItem.itemIcon);
        }

        //Disables tooltip when inventory is closed
        showTooltip = false;
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;

        //Draws slots
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(x * iconSize, y * iconSize, iconSize, iconSize);
                GUI.Box(new Rect(x * iconSize, y * iconSize, iconSize, iconSize), "", skin.GetStyle("Slot"));

                slots[i] = inventory[i];
                Item item = slots[i];

                if (item.itemName != null)
                {
                    GUI.DrawTexture(slotRect, item.itemIcon);
                    GUI.Label(slotRect, item.itemQuantity.ToString());

                    if (slotRect.Contains(e.mousePosition))
                    {


                        if (item.itemName != null && item.itemIcon != null)
                        {
                            GUI.DrawTexture(slotRect, item.itemIcon);
                            GUI.Label(slotRect, item.itemQuantity.ToString()); //TODO: ITEMQUANT

                            if (slotRect.Contains(e.mousePosition))
                            {

                                //Drag item
                                if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = item;
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

                                if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
                                {
                                    if (item.itemType == Item.ItemType.Consumable)
                                    {
                                        UseConsumable(item, i, true);
                                    }
                                }

                                if (!draggingItem)
                                {
                                    tooltip = "<color=#ffffff><b>" + item.itemName + " </b> \n\n" + item.itemDesc + "</color>\n\n" + "Amount: " + item.itemQuantity;
                                    //Show tooltip when not dragging
                                    if (!draggingItem)
                                    {
                                        tooltip = "<color=#ffffff><b>" + item.itemName + " </b> \n\n" + item.itemDesc +
                                                  "</color>\n\n" + "Amount: " + (item.itemQuantity); //TODO: ITEMQUANT
                                        showTooltip = true;
                                    }
                                }
                            }
                            else
                            {   // Allows to drag an item to an empty slot

                                {
                                    // Allows to drag an item to an empty slot
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
                }
                if (item.itemName != null && item.itemIcon != null)
                {
                    GUI.DrawTexture(slotRect, item.itemIcon);
                    GUI.Label(slotRect, item.itemQuantity.ToString()); //TODO: ITEMQUANT

                    if (slotRect.Contains(e.mousePosition))
                    {
                        //Drag item
                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = item;
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

                        //Use consumable
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
                        {
                            if (item.itemType == Item.ItemType.Consumable)
                            {
                                UseConsumable(item, i, true);
                            }
                        }

                        //Show tooltip when not dragging
                        if (!draggingItem)
                        {
                            tooltip = "<color=#ffffff><b>" + item.itemName + " </b> \n\n" + item.itemDesc +
                                      "</color>\n\n" + "Amount: " + (item.itemQuantity); //TODO: ITEMQUANT
                            showTooltip = true;
                        }
                    }
                }
                else
                {
                    // Allows to drag an item to an empty slot
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


    // RemoveItem, item name and the amount to delete.
    void RemoveItem(string itemName, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == itemName)
            {
                // if the inventory contains more than amount needed to item should not be deleted, but rather decrease in quantity.
                if (inventory[i].itemQuantity > amount)
                {
                    inventory[i].itemQuantity -= amount;
                    break;
                }
                // every other situation the item needs to be removed from inventory. Adding a empty item in its place.
                else
                {
                    inventory[i] = new Item();
                    break;
                }
            }
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id && inventory[i].isStackable)
            {
                inventory[i].itemQuantity += 1;
                break;
            }

            //Find empty inventory space
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

    public bool InventoryContains(string material, int requiredAmount)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == material && item.itemQuantity >= requiredAmount)
            {
                return true;
            }
        }
        return false;
    }

    // NOT USED?
    public int GetItemQuantity(int id)
    {
        foreach (Item item in inventory)
        {
            if (item.itemID == id)
            {
                return item.itemQuantity;
            }
        }

        return 0;
    }

    void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.itemID)
        {
            //Meat
            case 2:
                playerHealthManager.HealPlayer(10);
                break;
        }

        if (deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            PlayerPrefs.SetInt("Inventory " + i, inventory[i].itemID);
        }
    }

    void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0
                ? database.items[PlayerPrefs.GetInt("Inventory " + i)]
                : new Item();
        }
    }

    public bool GetInventoryStatus()
    {
        return isInventoryOpen;
    }
}
