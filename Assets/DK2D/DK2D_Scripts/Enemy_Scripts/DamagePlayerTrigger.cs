using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerTrigger : PlayerEnterExitTrigger
{
    [SerializeField]
    float damageAmount;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // damages player if the player enters the trigger
        player.stats.AdjustHealth(-damageAmount);
    }
}
