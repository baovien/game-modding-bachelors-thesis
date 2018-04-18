using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Rewrite this to a general solution, additionally layers
public class Harvest : MonoBehaviour {

    public int damageToGive;
    
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
