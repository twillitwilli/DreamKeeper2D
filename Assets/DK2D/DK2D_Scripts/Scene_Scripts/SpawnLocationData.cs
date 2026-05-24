using UnityEngine;

[System.Serializable]
public class SpawnLocationData
{
    public string spawnName;

    public Transform spawnPosition;

    // rendering objects
    public GameObject[]
        enableObjects,
        disableObjects;
}
