using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerTrigger : PlayerEnterExitTrigger
{
    [SerializeField]
    Transform _playerMovePosition;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        base.PlayerEnteredTrigger(player);

        player.transform.position = _playerMovePosition.position;
    }
}
