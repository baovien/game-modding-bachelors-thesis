using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
    public Text itemName;
    public Image itemIcon;
    public Button craftBtn;

    private CraftingScrollList craftingScrollList;
    private Inventory inventory;
    private bool state;

    // Use this for initialization
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        craftingScrollList = GameObject.FindGameObjectWithTag("CraftingScrollList").GetComponent<CraftingScrollList>();

        craftBtn.onClick.AddListener(HandleClick);
    }

    public void SetItemName(string craftableItemName)
    {
        itemName.text = craftableItemName;
    }

    public string GetItemName()
    {
        return itemName.text;
    }

    public void SetItemIcon(Sprite sprite)
    {
        itemIcon.sprite = sprite;
    }
    
    public void HandleClick()
    {
        if (itemName.text != "Select an item")
        {
            inventory.Craft(itemName.text);
            craftingScrollList.RefreshDisplay();   
        }
    }
}