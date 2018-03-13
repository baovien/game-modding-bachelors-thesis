using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int wood;
    public int stone;
    
    // Use this for initialization
    void Start ()
    {
        wood = 0;
        stone = 0;
    }

    public void AddResource(string resourceTag, int resourceToAdd)
    {
        switch (resourceTag)
        {
            case "Tree":
                wood += resourceToAdd;
                Debug.Log(wood);

                break;
            case "Rock":
                stone += resourceToAdd;
                break;
            default:
                Debug.Log("No resource!");
                break;
        }
    }


}
