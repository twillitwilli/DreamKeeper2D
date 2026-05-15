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
        SceneManager.LoadScene(SceneSelector.GetScene(sceneName));
    }
}
