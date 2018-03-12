using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : MonoBehaviour {

    public int damageToGive;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            other.gameObject.GetComponent<TreeManager>().ChopTree(damageToGive);
            Debug.Log("HIT");
        }
    }
}
