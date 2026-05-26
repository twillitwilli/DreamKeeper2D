using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField]
    SceneSelector.SceneName sceneName;

    private void Start()
    {
        // gets game manager reference
        GameManager manager = GameManager.Instance;

        // sets manager current scene
        manager.currentScene = sceneName;

        // start music
        manager.audioController.PlayMusic();
    }
}
