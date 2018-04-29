using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY, hotbarX;
    public List<Item> hotBar = new List<Item>();
    public List<Item> hotBarSlots = new List<Item>();
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public List<string> craftable = new List<string>();
    public GUISkin skin;

    private PlayerHealthManager playerHealthManager;
    private ItemDatabase database;
    private RecipeDatabase recipeDatabase;
    private CraftingScrollList craftingScrollList;

    private bool showInventory;
    private bool showTooltip;
    private string tooltip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;
    private int iconSize;
    private bool isInventoryOpen;
    
    private int selectableItemsTotal;
    private int selectedItemID;
    private Item selectedItem;
    
    

    // Use this for initialization
    void Start()
    {
        playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        recipeDatabase = GameObject.FindGameObjectWithTag("RecipeDatabase").GetComponent<RecipeDatabase>();
        craftingScrollList = GameObject.FindGameObjectWithTag("CraftingScrollList").GetComponent<CraftingScrollList>();


        iconSize = 40;
        SelectedItem = new Item();

        // Fill the slots list with empty items.
        for (int i = 0; i < slotsX * slotsY; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        for (int i = 0; i < hotbarX; i++)
        {
            hotBarSlots.Add(new Item());
            hotBar.Add(new Item());
        }
        

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

            if (draggingItem)
            {
                inventory[prevIndex] = draggedItem;
                draggingItem = false;
                draggedItem = null;
            }
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].itemQuantity < 1)
            {
                inventory[i] = new Item();
                UpdateCraftable();
            }
        }

            // No current item
            if (SelectedItem == null)
        {
            // ensure hotbar list is ready
            if (hotBar[selectedItemID] != null)
            {
                // Get a new selcted item using ID
                SelectedItem = hotBar[selectedItemID];
            }
        }
        
        // Using scrollwheel to traverse the list of items in hotbar. 
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (mousewheel != 0)
        {
            selectableItemsTotal = hotBar.Count - 1;

            if (mousewheel > 0)
            {
                selectedItemID--;
                if (selectedItemID < 0)
                {
                    selectedItemID = selectableItemsTotal;
                }
            }
            else if (mousewheel < 0)
            {
                selectedItemID++;

                if (selectedItemID > selectableItemsTotal)
                {
                    selectedItemID = 0;
                }
            }

            SelectedItem = hotBar[selectedItemID];
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItemID = 0;
            SelectedItem = hotBar[selectedItemID];
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedItemID = 1;
            SelectedItem = hotBar[selectedItemID];
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedItemID = 2;    
            SelectedItem = hotBar[selectedItemID];
        }else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedItemID = 3;    
            SelectedItem = hotBar[selectedItemID];
        }else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedItemID = 4;  
            SelectedItem = hotBar[selectedItemID];
        }
        
    }

    //Unity method to draw in screen space
    void OnGUI()
    {
                
        tooltip = "";
        GUI.skin = skin;

        DrawHotBar();

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
            if (Event.current.mousePosition.y < Screen.width / 2f)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 150, 150), tooltip,skin.GetStyle("Tooltip"));
            }
            else
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y - 150, 150, 150), tooltip,skin.GetStyle("Tooltip"));
            }
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

    void DrawHotBar()
    {
        Event e = Event.current;
        int i = 0;

        //Draws hotbar

        for (int x = 0; x < hotbarX; x++)
        {
            
            Rect slotRect = new Rect(Screen.width / 2.0f - iconSize * 2.5f + iconSize * x, Screen.height - iconSize,iconSize, iconSize);
          
            GUI.Box(new Rect(Screen.width / 2.0f - iconSize * 2.5f + iconSize * x, Screen.height - iconSize, iconSize, iconSize), "", skin.GetStyle("Slot"));

            hotBarSlots[i] = hotBar[i];
            Item item = hotBarSlots[i];
            
            
            if (item.itemName != null && item.itemIcon != null)
            {
                //TODO: Fix so that empty slots also are selected
                if (SelectedItem.itemID == hotBar[i].itemID)
                {
                    GUI.Box(new Rect(Screen.width / 2.0f - iconSize * 2.5f + iconSize * x, Screen.height - iconSize, iconSize,iconSize), "", skin.GetStyle("SelectedSlot"));
                }
                
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
                        hotBar[i] = new Item();
                    }

                    //Swap items position
                    if (e.type == EventType.MouseUp && draggingItem)
                    {
                        hotBar[prevIndex] = hotBar[i];
                        hotBar[i] = draggedItem;
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
                                  "</color>\n\n" + "Amount: " + item.itemQuantity; //TODO: ITEMQUANT
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
                        hotBar[i] = draggedItem;
                        draggingItem = false;
                        draggedItem = null;
                    }
                }
            }
            i++;
        }
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
                                      "</color>\n\n" + "Amount: " + item.itemQuantity; //TODO: ITEMQUANT
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
                // if the inventory contains more than amount needed the item should not be deleted, but rather decrease in quantity.
                if (inventory[i].itemQuantity > amount)
                {
                    inventory[i].itemQuantity -= amount;
                    UpdateCraftable();
                    break;
                }
                // every other situation the item needs to be removed from inventory. Adding a empty item in its place.
                
                inventory[i] = new Item();
                UpdateCraftable();
                break;
            }
        }

        for (int i = 0; i < hotBar.Count; i++)
        {
            if (hotBar[i].itemName == itemName)
            {
                // if the inventory contains more than amount needed the item should not be deleted, but rather decrease in quantity.
                if (hotBar[i].itemQuantity > amount)
                {
                    hotBar[i].itemQuantity -= amount;
                    UpdateCraftable();
                    break;
                }
                // every other situation the item needs to be removed from inventory. Adding a empty item in its place.
                
                hotBar[i] = new Item();
                UpdateCraftable();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        
        if (itemToAdd.isStackable && InventoryContains(itemToAdd.itemID))
        {
            foreach (var item in inventory)
            {
                if (item.itemID == id)
                {
                    item.itemQuantity += 1;
                    UpdateCraftable();
                    break;
                }
            }
        }else if (itemToAdd.isStackable && HotBarContains(itemToAdd.itemID))
        {
            foreach (var item in hotBar)
            {
                if (item.itemID == id)
                {
                    item.itemQuantity += 1;
                    break;
                }
            }
        }
        else
        {    //Find empty inventory space
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == -1)
                {
                    inventory[i] = itemToAdd;
                    UpdateCraftable();
                    //inventory[i].itemQuantity = 1; //TODO: Itemquantity varierer noen ganger. Feature or bug?

                    break;
                }
            }
        }
        //After an item is added/removed we need to check craftable again.
        UpdateCraftable();
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
        
        foreach (Item item in hotBar)
        {
            if (item.itemName == material && item.itemQuantity >= requiredAmount)
            {
                return true;
            }
        }
        
        return false;
    }
    
    public bool InventoryContains(int id)
    {
        foreach (Item item in inventory)
        {
            if (item.itemID == id)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool HotBarContains(int id)
    {
        foreach (Item item in hotBar)
        {
            if (item.itemID == id)
            {
                return true;
            }
        }

        return false;
    }

    void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.itemID)
        {
            //Meat
            case 0:
                playerHealthManager.HealPlayer(10);
                Debug.Log("Consume");
                break;
            default:
                break;
        }

        if (deleteItem)
        {
            hotBar[slot] = new Item(); 
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
    
    // When the player craft the item we need to do something ...
    public void Craft(string item)
    {
        int index = recipeDatabase.recipes.IndexOf(recipeDatabase.recipes.FirstOrDefault(p => p.itemName == item));
        int indexForItem = database.items.IndexOf(database.items.FirstOrDefault(p => p.itemName == item));

        foreach (var mat in recipeDatabase.recipes[index].items.Keys)
        {
            // mat is material, items[mat] is the amount
            RemoveItem(mat, recipeDatabase.recipes[index].items[mat]);
        }
        
        AddItem(indexForItem);
        UpdateCraftable();
    }

    void UpdateCraftable()
    {
        for (int i = 0; i < recipeDatabase.recipes.Count; i++)
        {
            bool canCraft = true;
            foreach (var mat in recipeDatabase.recipes[i].items.Keys)
            {
                if (!InventoryContains(mat, recipeDatabase.recipes[i].items[mat]))
                {
                    //if one of the materials are missing the item can not be crafted, set false for that recipe.                   
                    canCraft = false;
                    if (CheckCraftable(recipeDatabase.recipes[i].itemName))
                    {
                        craftable.Remove(recipeDatabase.recipes[i].itemName);
                    }
                }
            }
            if (canCraft)
            {
                // If something is craftable, add it to the list containing items we can craft.
                if (!CheckCraftable(recipeDatabase.recipes[i].itemName))
                {
                    craftable.Add(recipeDatabase.recipes[i].itemName);
                    Debug.Log(recipeDatabase.recipes[i].itemName + " is craftable!");
                        
                    //Refresh craftingwindow to show new craftables
                    craftingScrollList.RefreshDisplay();
                }
                else
                {
                    //Debug.Log("An item is craftable but is already ready in the craftable list");
                }
            }
        }
    }
    
    public bool CheckCraftable(string itemName)
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

    public bool CraftableContains(string item)
    {
        if (craftable.Any(i => i.Contains(item)))
        {
            return true;
        }

        return false;
    }

    public Item SelectedItem
    {
        get { return selectedItem; }

        set { selectedItem = value; }
    }
    
}