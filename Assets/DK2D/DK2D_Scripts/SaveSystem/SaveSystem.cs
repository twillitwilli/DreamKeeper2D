using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BinaryPlayerSaveLoad : MonoBehaviour
{
    GameManager _gameManager;
    PlayerController _player;
    PlayerStats _playerStats;
    PlayerStatsData _statsData;

    public void SetReferences()
    {
        // Game Manager reference
        _gameManager = GameManager.Instance;

        // Set player reference
        _player = PlayerController.Instance;

        // Set player stats reference
        _playerStats = _player.stats;

        // Set data for player stats
        _statsData = _playerStats.playerStats;
    }

    public void SaveGame()
    {
        BinarySaveSystem.SaveData(CreateSaveData());
    }

    public void LoadGame()
    {
        BinarySaveData loadedData = BinarySaveSystem.LoadData();

        // checkes if there is a save file to load
        if (loadedData != null)
            LoadSavedData(loadedData);

        else Debug.Log("No Save File Found");
    }

    public void DeleteSaveFile()
    {
        BinarySaveSystem.DeleteFileSave();
    }

    // ========= Creates Save File ============

    private BinarySaveData CreateSaveData()
    {
        // creates new save data file
        BinarySaveData newData = new BinarySaveData();

        // Sets player stats data
        newData.playerStats = _statsData;

        return newData;
    }

    // =========== Load Save File =============

    private void LoadSavedData(BinarySaveData loadedData)
    {
        _statsData = loadedData.playerStats;
    }
}
