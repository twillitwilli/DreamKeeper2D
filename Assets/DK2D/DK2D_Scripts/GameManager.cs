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

    public GameOverScreen gameOver;

    GameObject _loadedPlayerPrefab;
    PlayerController _player;
    LoadScene _sceneLoader;

    private void Start()
    {
        // gets reference to scene loader
        _sceneLoader = GetComponent<LoadScene>();
    }

    public void SpawnPlayer(bool newPlayer)
    {
        // loads player if not already loaded
        if (_loadedPlayerPrefab == null)
        {
            // spawns player and sets reference
            _loadedPlayerPrefab = Instantiate(_playerPrefab);

            // sets parent to game manager
            _loadedPlayerPrefab.transform.SetParent(this.gameObject.transform);

            // set player controller reference
            _player = PlayerController.Instance;

            // if new player is player, default dream state to true
            if (newPlayer)
                _player.stats.playerStats.dreamState = true;

            // sets save system references
            saveSystem.SetReferences();
        }
    }

    public void GameOver()
    {
        // destroy player object
        Destroy(_loadedPlayerPrefab);

        // disable game over screen
        gameOver.gameObject.SetActive(false);

        // set to title screen
        _sceneLoader.sceneName = SceneSelector.SceneName.TitleScreen;

        // load scene
        _sceneLoader.SceneLoader();
    }

    public void CheckPlayerDreamState()
    {
        _player.stats.SwitchHealthDisplays();
    }
}
