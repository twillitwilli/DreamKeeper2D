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

    public void SaveGame(int saveFile)
    {
        BinarySaveSystem.SaveData(CreateSaveData(), saveFile);
    }

    public void LoadGame(int saveFile)
    {
        BinarySaveData loadedData = BinarySaveSystem.LoadData(saveFile);

        // checkes if there is a save file to load
        if (loadedData != null)
            LoadSavedData(loadedData);

        else Debug.Log("No Save File Found");
    }

    public void DeleteSaveFile(int saveFile)
    {
        BinarySaveSystem.DeleteFileSave(saveFile);
    }

    // ========= Creates Save File ============

    private BinarySaveData CreateSaveData()
    {
        // creates new save data file
        BinarySaveData newData = new BinarySaveData();

        // sets save file
        newData.saveFile = _gameManager.saveFile;

        // Sets player stats data
        newData.playerStats = _statsData;

        return newData;
    }

    // =========== Load Save File =============

    private void LoadSavedData(BinarySaveData loadedData)
    {
        _gameManager.saveFile = loadedData.saveFile;

        _statsData = loadedData.playerStats;
    }
}
