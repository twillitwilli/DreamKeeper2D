using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SceneSelector.SceneName sceneName;

    public void SceneLoader()
    {
        // changes the scene to the selected scene
        SceneManager.LoadScene(SceneSelector.GetScene(sceneName));

        // check player dream state
        GameManager.Instance.CheckPlayerDreamState();
    }
}
