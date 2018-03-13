using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Slider healthBar;
	public Text HPText;
    public Text woodText;
	public Text stoneText;
	public PlayerHealthManager playerHealth;
    public PlayerInventory playerInventory;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;
        HPText.text = "" + playerHealth.playerCurrentHealth;
        woodText.text = "Wood: " + playerInventory.wood;
		stoneText.text = "Stone: " + playerInventory.stone;
	}
}
