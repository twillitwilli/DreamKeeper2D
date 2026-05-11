using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    PlayerController _player;

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }
}
