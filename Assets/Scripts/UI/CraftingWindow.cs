using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
    public Text itemName;
    public Image itemIcon;
    public Button craftBtn;

    private Inventory inventory;
    private bool state;

    // Use this for initialization
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        craftBtn.onClick.AddListener(HandleClick);
    }

    public void SetItemName(Item item)
    {
        itemName.text = item.itemName;
    }

    public void SetItemIcon(Item item)
    {
        itemIcon.sprite = Sprite.Create(item.itemIcon, new Rect(0, 0, item.itemIcon.width, item.itemIcon.height),
            new Vector2(0.5f, 0.5f));
    }
    
    public void HandleClick()
    {
        inventory.Craft(itemName.text);
        Debug.Log("Crafted " + itemName.text);
    }
}