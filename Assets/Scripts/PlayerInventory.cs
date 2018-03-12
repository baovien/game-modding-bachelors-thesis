using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int wood;
    
    // Use this for initialization
    void Start ()
    {
        wood = 0;
    }

    public void AddWood(int woodToAdd)
    {
        wood += woodToAdd;
    }

}
