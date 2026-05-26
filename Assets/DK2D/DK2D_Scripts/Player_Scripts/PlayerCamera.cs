using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerController _player;

    Animator _animator;

    private void Start()
    {
        // gets reference to player
        _player = PlayerController.Instance;

        //gets animator reference
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // follows the player position
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }

    public void ScreenShake()
    {
        _animator.Play("CameraShake");
    }
}
