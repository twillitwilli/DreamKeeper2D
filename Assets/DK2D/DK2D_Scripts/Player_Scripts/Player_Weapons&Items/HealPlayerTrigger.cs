using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayerTrigger : PlayerEnterExitTrigger
{
    [SerializeField]
    float healAmount;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // heal player
        player.stats.AdjustHealth(healAmount);
    }
}
