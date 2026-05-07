using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerController _player;

    private void Start()
    {
        // gets reference to player
        _player = PlayerController.Instance;
    }

    private void Update()
    {
        // follows the player position
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }
}
