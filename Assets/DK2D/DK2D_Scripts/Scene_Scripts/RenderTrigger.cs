using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTrigger : PlayerEnterExitTrigger
{
    [SerializeField]
    GameObject[]
        _enableObjects,
        _disableObjects;

    [SerializeField]
    Transform _moveToPosition;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        base.PlayerEnteredTrigger(player);

        // if there are objects to enable, enables all objects
        if (_enableObjects != null)
        {
            for (int i = 0; i < _enableObjects.Length; i++)
            {
                _enableObjects[i].SetActive(true);
            }
        }

        // if there are objects to disable, disables all objects 
        if (_disableObjects != null)
        {
            for (int i = 0; i < _disableObjects.Length; i++)
            {
                _disableObjects[i].SetActive(false);
            }
        }

        // moves player to new postion
        player.transform.position = _moveToPosition.position;
    }
}
