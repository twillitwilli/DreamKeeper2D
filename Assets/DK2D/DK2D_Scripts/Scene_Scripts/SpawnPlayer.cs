using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    bool _isNewPlayer;

    public void PlayerSpawner()
    {
        // Spawn Player
        GameManager.Instance.SpawnPlayer(_isNewPlayer);
    }
}
