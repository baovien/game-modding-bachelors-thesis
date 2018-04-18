using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;
	public int resourceToGive;
	public GameObject pickableObject;
	
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentHealth <= 0)
		{			
			//Instantiate pickable gameobject after harvesting resource
			var instaniatedPrefab = Instantiate(pickableObject, transform.position, transform.rotation);
			instaniatedPrefab.transform.localScale = new Vector3(2, 2, transform.position.z); //Scales up the object
			
			Destroy(gameObject);
		}
	}
	
	public void HarvestResource(int damageToGive)
	{
		currentHealth -= damageToGive;
	}
}
