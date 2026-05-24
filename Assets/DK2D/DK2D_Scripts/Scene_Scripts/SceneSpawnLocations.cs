using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnLocations : MonoBehaviour
{
    public SpawnLocationData[] spawnLocations;

    GameManager _gameManager;

    private void Awake()
    {
        // sets reference to game manager
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        // sets reference to player
        PlayerController player = PlayerController.Instance;

        // moves player to spawn location
        player.transform.position = spawnLocations[_gameManager.sceneSpawnLocation].spawnPosition.position;

        // render objects into scene
        RenderObjects();

        // disable objects from scene
        DisableObjects();
    }

    void RenderObjects()
    {
        
    }

    void DisableObjects()
    {

    }
}
