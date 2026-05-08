using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public void OpenChest()
    {
        Debug.Log("Opened Chest");

        // removes the chest script from the object
        Destroy(this);
    }
}
