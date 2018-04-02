using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;
	public int resourceToGive;
	public GameObject pickableObject;
	

	private PlayerInventory _playerInventory;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;
		_playerInventory = FindObjectOfType<PlayerInventory>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentHealth <= 0)
		{
			_playerInventory.AddResource(gameObject.tag, resourceToGive);
						
			Destroy(gameObject);
		}
	}
	
	public void HarvestResource(int damageToGive)
	{
		currentHealth -= damageToGive;
	}
}
