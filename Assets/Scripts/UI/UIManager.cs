using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Slider healthBar;
	public Text HPText;
	public Text ModsText;
	public PlayerHealthManager playerHealth;
	private RuntimeCompiler rtc;

	private void Start()
	{
		rtc = GameObject.FindGameObjectWithTag("ModManager").GetComponent<RuntimeCompiler>();
				
		foreach (var mod in rtc.navn)
		{
			ModsText.text += mod + "\n";
		}
		
	}

	// Update is called once per frame
	void Update ()
	{
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;
        HPText.text = "" + playerHealth.playerCurrentHealth;
	}
}
