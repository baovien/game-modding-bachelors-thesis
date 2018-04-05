using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;

    public float flashLength;
    private bool flashActive;
    private float flashCounter;

    private SpriteRenderer playerSprite;
    //private SFXManager sfxMan;

    // Use this for initialization
    void Start () {
        playerCurrentHealth = playerMaxHealth;
        playerSprite = GetComponent<SpriteRenderer>();
        //sfxMan = FindObjectOfType<SFXManager>();

    }

    // Update is called once per frame
    void Update () {
        if(playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            //sfxMan.playerDead.Play();
        }
           
        //Player flashes if hit by enemy
        if (flashActive)
        {
            if(flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

            } else if (flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);

            } else if (flashCounter > 0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

            }else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }
    
    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
        Debug.Log("AU DET SVIR");
        flashActive = true;
        flashCounter = flashLength;
        //sfxMan.playerHurt.Play();
    }

    public void HealPlayer(int hpToHeal)
    {
        playerCurrentHealth += hpToHeal;
    }
}
