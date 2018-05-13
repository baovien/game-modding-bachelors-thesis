using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class modEnemy2 : MonoBehaviour, IEnemy
{	
	public int AttackDamage{get{return 5;} }
    public float HitPoints{get{return 100f;} }
    public float MoveSpeed{get{return 1.5f;} }
	// Instantiate the mod and create game object when the game boots
	public static void InstantiateMe()
	{
		//Unity game objects.
		GameObject modE = new GameObject("modEnemy2");
		//Add this script to the created game object.
		modE.AddComponent<modEnemy2>();
		//Create a rigidbody.
		Rigidbody2D modBody = modE.AddComponent<Rigidbody2D>();
		modE.AddComponent<BoxCollider2D>();
		//Create a sprite
		Sprite sprite = Resources.Load<Sprite>("player_hand");		
		SpriteRenderer modSprite = modE.AddComponent<SpriteRenderer>();
		modSprite.sprite = sprite;
		modSprite.sortingOrder = 3;
		modBody.gravityScale = 0.0f;
		
	}
	// These next functions are unity's, start() is called when the script is first in use, eg spawning this object. Update() is called once per frame.
    // Unity function 
    void Start()
    {		
    }
	
    void Update()
    {
		//Decide some movement
		transform.Translate(0,0,0);
    }
	
}
