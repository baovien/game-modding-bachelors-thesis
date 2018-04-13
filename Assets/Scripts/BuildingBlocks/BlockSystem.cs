// System required for [Serializable] attribute.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockSystem : MonoBehaviour
{
    private ItemDatabase database;

    // Array to store all blocks created in Awake()
    [HideInInspector]
    public Block[] allBlocks;

    private void Awake()
    {
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        List<Item> blockList = database.items.Where(o => o.itemType == Item.ItemType.Block) as List<Item>;
    }

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            for (int i = 0; i < allBlocks.Length; i++)
            {
                GameObject newPickup = new GameObject(allBlocks[i].blockName, typeof(SpriteRenderer));
                newPickup.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                newPickup.GetComponent<SpriteRenderer>().sprite = allBlocks[i].blockSprite;
                newPickup.AddComponent<BoxCollider2D>();
                newPickup.AddComponent<Rigidbody2D>();
                newPickup.tag = "Pickup";
                newPickup.transform.position = Vector2.zero + (Vector2.up * i);
            }
        }
    }
}


// Block class to store the final Block type data.
public class Block
{
    public int blockID;
    public string blockName;
    public Sprite blockSprite;
    public bool isSolid;
    public int amountInInventory;

    public Block(int id, string myName, Sprite mySprite, bool amISolid)
    {
        blockID = id;
        blockName = myName;
        blockSprite = mySprite;
        isSolid = amISolid;
        amountInInventory = 0;
    }
}

// Custom struct for Block type.
[Serializable]
public struct BlockType
{
    // Main, differing variables for each block type.
    public string blockName;
    public Sprite blockSprite;
    public bool blockIsSolid;
}