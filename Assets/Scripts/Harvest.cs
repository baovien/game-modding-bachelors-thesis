using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour {

    public int damageToGive;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Rock"))
        {
            other.gameObject.GetComponent<ResourceManager>().HarvestResource(damageToGive);
            Debug.Log("HIT");
        }
    }
}
