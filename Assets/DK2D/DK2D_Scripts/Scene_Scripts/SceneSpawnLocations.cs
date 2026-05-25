using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnLocations : MonoBehaviour
{
    public SpawnLocationData[] spawnLocations;

    GameManager _gameManager;

    int _currentSpawn;

    private void Awake()
    {
        // sets reference to game manager
        _gameManager = GameManager.Instance;

        // set current spawn location
        _currentSpawn = _gameManager.sceneSpawnLocation;
    }

    private void Start()
    {
        // sets reference to player
        PlayerController player = PlayerController.Instance;

        // moves player to spawn location
        player.transform.position = spawnLocations[_currentSpawn].spawnPosition.position;

        // render objects into scene
        RenderObjects();

        // disable objects from scene
        DisableObjects();
    }

    void RenderObjects()
    {
        // checks to see if there is anything in the array
        if (spawnLocations[_currentSpawn].enableObjects.Length > 0)
        {
            // enables all objects in the enableObjects array
            for (int i = 0; i < spawnLocations[_currentSpawn].enableObjects.Length; i++)
            {
                spawnLocations[_currentSpawn].enableObjects[i].SetActive(true);
            }
        }
    }

    void DisableObjects()
    {
        // checks to see if there is anything in the array
        if (spawnLocations[_currentSpawn].disableObjects.Length > 0)
        {
            // disables all objects in the disableObjects array
            for (int i = 0; i < spawnLocations[_currentSpawn].disableObjects.Length; i++)
            {
                spawnLocations[_currentSpawn].disableObjects[i].SetActive(false);
            }
        }
    }
}
