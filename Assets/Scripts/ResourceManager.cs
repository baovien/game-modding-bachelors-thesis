using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int resourceToGive;
    public GameObject pickableObject;

    private bool isQuitting;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {

            Destroy(gameObject);
        }
    }
    
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    // Will be called just prior to destruction of the gameobject to which this script is attached
    void OnDestroy()
    {
        //Makes sure that the resources does not instantiate objects when destroyed upon quitgame.
        if (!isQuitting)
        {
            //Instantiate pickable gameobject after harvesting resource
            var instantiatedPrefab = Instantiate(pickableObject, transform.position, transform.rotation);
            instantiatedPrefab.transform.localScale = new Vector3(2, 2, transform.position.z); //Scales up the object
        }
    }

    public void HarvestResource(int damageToGive)
    {
        currentHealth -= damageToGive;
    }
}