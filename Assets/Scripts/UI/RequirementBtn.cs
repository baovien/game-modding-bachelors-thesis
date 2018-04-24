using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RequirementBtn : MonoBehaviour
{
    public Text itemName;
    public Text itemAmount;
    public Image itemIcon;
    
    public void Setup(string reqName, int reqAmount, Texture2D icon)
    {
        itemName.text = reqName;
        itemAmount.text = reqAmount.ToString();
        itemIcon.sprite = Sprite.Create(icon,new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
    }
}