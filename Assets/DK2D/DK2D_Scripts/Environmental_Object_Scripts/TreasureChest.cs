using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _renderer;

    [SerializeField]
    Sprite _openedChestSprite;

    [SerializeField]
    GameObject _chestLootPrefab;

    public void OpenChest()
    {
        Debug.Log("Opened Chest");

        // changes the sprite of the chest to the opened sprite chest
        if (_renderer != null)
            _renderer.sprite = _openedChestSprite;

        if (_chestLootPrefab != null)
            Instantiate(_chestLootPrefab, transform.position, transform.rotation);

        // removes the chest script from the object
        Destroy(this);
    }
}
