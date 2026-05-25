using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorIsLocked;

    [SerializeField]
    Transform _movePlayerToLocation;

    [SerializeField]
    GameObject[]
        _disbleObjects,
        _enableObjects;

    public void OpenDoor(PlayerController player)
    {
        if (doorIsLocked)
        {
            Debug.Log("Door is locked");

            GameObject lockedPrefab = LockIndicatorPool.Instance.GetItem();

            lockedPrefab.transform.position = transform.position;
        }
            

        else
        {
            // enable and disable objects
            RenderObjects();

            // move player to other side of door
            player.transform.position = _movePlayerToLocation.position;
        }
            
    }

    void RenderObjects()
    {
        // disable all objects 
        if ( _disbleObjects.Length > 0 )
        {
            for (int i = 0;  i < _disbleObjects.Length; i++)
            {
                _disbleObjects[i].SetActive(false);
            }
        }

        // enable all objects
        if ( _enableObjects.Length > 0 )
        {
            for (int i = 0; i < _enableObjects.Length; i++)
            {
                _enableObjects[i].SetActive(true);
            }
        }
    }
}
