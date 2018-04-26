using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftableItemBtn : MonoBehaviour
{
    public Button buttonComponent;

    private Item craftableItem;
    private RequirementScrollList reqScrollList;
    private CraftingWindow craftingWindow;

    // Use this for initialization
    void Start()
    {
        craftingWindow = GameObject.FindGameObjectWithTag("CraftingWindow").GetComponent<CraftingWindow>();
        reqScrollList = GameObject.FindGameObjectWithTag("RequirementScrollList").GetComponent<RequirementScrollList>();
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem)
    {
        craftableItem = currentItem;
        buttonComponent.GetComponent<Image>().sprite = Sprite.Create(craftableItem.itemIcon,
            new Rect(0, 0, craftableItem.itemIcon.width, craftableItem.itemIcon.height), new Vector2(0.5f, 0.5f));
    }

    public void HandleClick()
    {
        craftingWindow.SetItemIcon(Sprite.Create(craftableItem.itemIcon, new Rect(0, 0, craftableItem.itemIcon.width, craftableItem.itemIcon.height),
            new Vector2(0.5f, 0.5f)));
        craftingWindow.SetItemName(craftableItem.itemName);
        craftingWindow.craftBtn.interactable = true;
        reqScrollList.UpdateRequirements(craftableItem);
    }

}