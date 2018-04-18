using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandSlot : MonoBehaviour
{

	public int damageToGive;

	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	private Inventory inventory;
	
	// Use this for initialization
	void Start ()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Item item = inventory.GetSelectedItem();
		if (item.itemID != -1)
		{
			spriteRenderer.sprite = Sprite.Create(item.itemIcon, new Rect(0, 0, item.itemIcon.width, item.itemIcon.height), new Vector2(0.5f, 0.5f));
			boxCollider.size = new Vector2(spriteRenderer.bounds.size.x / transform.lossyScale.x, spriteRenderer.bounds.size.y / transform.lossyScale.y);
		}
	}

    
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Rock"))
		{
			other.gameObject.GetComponent<ResourceManager>().HarvestResource(damageToGive);
		}

		if (other.gameObject.CompareTag("Zombie"))
		{
			other.gameObject.GetComponent<Zombies>().HurtEnemy(10);
		}
	}
}
