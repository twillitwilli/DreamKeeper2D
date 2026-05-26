using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _gameManager;

    private void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(_gameManager);
    }
}
