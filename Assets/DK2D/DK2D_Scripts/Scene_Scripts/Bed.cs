using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField]
    bool _goToSleep;

    [SerializeField]
    SceneSelector.SceneName _moveTo;

    [SerializeField]
    int _spawnLocation;

    GameManager _gameManager;

    private void Awake()
    {
        // sets reference to game manager
        _gameManager = GameManager.Instance;
    }

    public void SleepInBed(PlayerController player)
    {
        player.movement.lockMovement = true;

        // player sleeping
        if (_goToSleep)
        {
            // sets player into dream state
            player.stats.playerStats.dreamState = true;

            // fills players health
            player.stats.AdjustHealth(99999);
        }
            

        // player waking up
        else
        {
            // wakes player up
            player.stats.playerStats.dreamState = false;

            // fills players health
            player.stats.AdjustHealth(99999);
        }

        // sets the scene the player will be moved to
        _gameManager.nextSceneName = _moveTo;

        // sets where the player will spawn at
        _gameManager.sceneSpawnLocation = _spawnLocation;

        // moves the player
        _gameManager.ChangePlayerScene();
    }
}
