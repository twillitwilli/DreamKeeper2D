using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[]
        enableObjects,
        disableObjects;

    public void NextMenu()
    {
        // checks to see if there are any objects to enable
        if (enableObjects != null)
        {
            // enables all objects
            foreach (GameObject obj in enableObjects)
                obj.SetActive(true);
        }

        // checks to see if there are any objects to disable
        if (disableObjects != null)
        {
            // disable all objects
            foreach (GameObject obj in disableObjects) 
                obj.SetActive(false);
        }
    }
}
