using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY;
    public int hotbarX;
    public List<Item> hotBar = new List<Item>();
    public List<Item> hotBarSlots = new List<Item>();
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;

    private PlayerHealthManager playerHealthManager;
    private ItemDatabase database;
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

    // Blocksystem and buildsystem references
    // private BuildSystem buildSys;

    // Use this for initialization
    void Start()
    {
        playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        // buildSys = GetComponent<BuildSystem>(); 

        iconSize = 40;

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
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
        AddItem(4);
    }

    void Update()
    {
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
        
        // No current block type set:
        if (selectedItem == null)
        {
            // ensure allBlocks array is ready
            if (hotBar[selectedItemID] != null)
            {
                // Get a new currentBlock using ID
                selectedItem = hotBar[selectedItemID];
            }
        }
        
        // Using scrollwheel to traverse the list of blocks. 
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

            selectedItem = hotBar[selectedItemID];
            Debug.Log("Selected item: " + selectedItem.itemName);
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

            if (GUI.Button(new Rect(40, 400, 100, 40), "Craft"))
            {
                CraftingCheck();
            }

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

    void CraftingCheck()
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            //TODO: Grab recipe, output craftable or not
            if (InventoryContains(i))
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (i == database.items[j].recipe[0] && GetItemQuantity(i) >= database.items[j].recipe[1])
                    {
                        RemoveItem(i, database.items[j].recipe[1]);
                        AddItem(j);
                        break;
                    }
                }
            }
        }
    }

    void DrawHotBar()
    {
        Event e = Event.current;
        int i = 0;

        //Draws hotbar

        for (int x = 0; x < hotbarX; x++)
        {
            
            Rect slotRect = new Rect(Screen.width / 2.0f - iconSize * 2.5f + iconSize * x, Screen.height - iconSize,iconSize, iconSize);
          
            GUI.Box(new Rect(Screen.width / 2.0f - iconSize * 2.5f + iconSize * x, Screen.height - iconSize, iconSize,iconSize), "", skin.GetStyle("Slot"));

            hotBarSlots[i] = hotBar[i];
            Item item = hotBarSlots[i];
            
            
            if (item.itemName != null && item.itemIcon != null)
            {
                //TODO: Fix so that empty slots also are selected
                if (selectedItem.itemID == hotBar[i].itemID)
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

    public void RemoveItem(int id, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id && inventory[i].isStackable)
            {
                Debug.Log("Removing " + amount + " " + inventory[i].itemName);
                database.items[id].itemQuantity -= amount;

                if (inventory[i].itemQuantity == 0)
                {
                    inventory[i] = new Item();
                }

                break;
            }

            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
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
                    itemToAdd.itemQuantity += 1;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                //Find empty inventory space
                if (inventory[i].itemID == -1)
                {
                    itemToAdd.itemQuantity = 1;
                    inventory[i] = itemToAdd;
                    //inventory[i].itemQuantity = 1; //TODO: Itemquantity varierer noen ganger. Feature or bug?

                    break;
                }
            }
        }

        /*
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id && inventory[i].isStackable)
            {
                database.items[id].itemQuantity += 1;
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
        */
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
            case 0:

                playerHealthManager.HealPlayer(10);
                Debug.Log("Consume");
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

    public bool GetInventoryStatus()
    {
        return isInventoryOpen;
    }
}