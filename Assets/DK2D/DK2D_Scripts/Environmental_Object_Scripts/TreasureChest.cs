using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    int 
        _sceneIndex,
        _index;

    [SerializeField]
    bool _isLocked;

    [SerializeField]
    SpriteRenderer _renderer;

    [SerializeField]
    Sprite _openedChestSprite;

    [SerializeField]
    GameObject _chestLootPrefab;

    public void OpenChest()
    {
        if (!_isLocked)
        {
            Debug.Log("Opened Chest");

            // changes the sprite of the chest to the opened sprite chest
            if (_renderer != null)
                _renderer.sprite = _openedChestSprite;

            // spawns loot prefab
            if (_chestLootPrefab != null)
                Instantiate(_chestLootPrefab, transform.position, transform.rotation);

            // sets the unlock status of chest to true
            GameManager.Instance.saveSystem.chestController.scene[_sceneIndex].unlockables[_index].isUnlocked = true;

            // removes interactable script from object
            Destroy(GetComponent<Interactable>());
        }

        else
        {
            Debug.Log("Chest Locked");

            GameObject lockedPrefab = LockIndicatorPool.Instance.GetItem();

            lockedPrefab.transform.position = transform.position;
        }
    }
}
