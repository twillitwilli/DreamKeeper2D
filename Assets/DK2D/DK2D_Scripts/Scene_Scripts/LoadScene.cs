using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    SceneSelector.SceneName sceneName;

    public void SceneLoader()
    {
        if (PlayerController.Instance == null)
            GameManager.Instance.SpawnPlayer();

        // changes the scene to the selected scene
        SceneManager.LoadScene(SceneSelector.GetScene(sceneName));
    }
}
