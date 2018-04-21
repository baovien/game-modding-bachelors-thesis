using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftableItemBtn : MonoBehaviour
{
    public Button buttonComponent;

    private Item item;
    private CraftingScrollList craftingScrollList;
    private CraftingWindow craftingWindow;

    // Use this for initialization
    void Start()
    {
        craftingWindow = GameObject.FindGameObjectWithTag("CraftingWindow").GetComponent<CraftingWindow>();
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem)
    {
        item = currentItem;
        buttonComponent.GetComponent<Image>().sprite = Sprite.Create(item.itemIcon,
            new Rect(0, 0, item.itemIcon.width, item.itemIcon.height), new Vector2(0.5f, 0.5f));
    }

    public void HandleClick()
    {
        craftingWindow.SetItemIcon(item);
        craftingWindow.SetItemName(item);
    }
}