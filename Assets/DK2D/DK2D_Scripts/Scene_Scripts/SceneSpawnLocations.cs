using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.AbstractClasses;

public class SceneSpawnLocations : MonoSingleton<SceneSpawnLocations>
{
    public Transform[] spawnLocations;

    public GameObject[] renderedObjects;

    private void Start()
    {
        // get loading index
        int loadIndex = GameManager.Instance.sceneSpawnLocation;

        // render objects during load
        renderedObjects[loadIndex].SetActive(true);

        // moves player to scene spawn location
        PlayerController.Instance.transform.position = spawnLocations[loadIndex].position;
    }
}
