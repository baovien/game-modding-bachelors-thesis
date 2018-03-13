using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Gjor om til en generell resource klasse. f.eks wood, stone, grass osv.

public class ResourceManager : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;
	public int resourceToGive;

	private PlayerInventory _playerInventory;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;
		_playerInventory = FindObjectOfType<PlayerInventory>();
		Debug.Log(_playerInventory);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0)
		{
			_playerInventory.AddResource(gameObject.tag, resourceToGive);

			Destroy(gameObject);
			Debug.Log("Resource harvested!");
		}
	}
	
	public void HarvestResource(int damageToGive)
	{
		currentHealth -= damageToGive;
	}
}
