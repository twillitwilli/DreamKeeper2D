using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BinarySaveData
{
    public int saveFile;

    public PlayerStatsData playerStats;
    public Unlockables 
        mainItemUnlocks,
        chestUnlocks;
}
