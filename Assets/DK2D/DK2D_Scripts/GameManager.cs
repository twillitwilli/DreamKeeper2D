using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.AbstractClasses;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    GameObject _playerPrefab;

    public int sceneSpawnLocation {  get; set; }

    public BinaryPlayerSaveLoad saveSystem;

    public void SpawnPlayer()
    {
        Instantiate(_playerPrefab);

        saveSystem.SetReferences();
    }
}
