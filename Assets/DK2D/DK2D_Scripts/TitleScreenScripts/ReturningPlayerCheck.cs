using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturningPlayerCheck : MonoBehaviour
{
    [SerializeField]
    Image _titleScreenImage;

    [SerializeField]
    Sprite _returningPlayerScreen;

    [SerializeField]
    GameObject
        _newPlayerButtons,
        _retuningPlayerButtons;

    private void Start()
    {
        // if a save file exists, change title screen to a returning player title screen
        if (BinarySaveSystem.SaveFileCheck())
        {
            // change image to the returning player screen
            _titleScreenImage.sprite = _returningPlayerScreen;

            // disable new player buttons
            _newPlayerButtons.SetActive(false);

            // enable returning Player buttons
            _retuningPlayerButtons.SetActive(true);
        }
    }
}
