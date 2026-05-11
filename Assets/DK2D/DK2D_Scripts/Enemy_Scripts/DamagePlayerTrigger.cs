using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.Interfaces;

public class DamagePlayerTrigger : PlayerEnterExitTrigger 
{
    [SerializeField]
    protected float _damageAmount;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // damages player if the player enters the trigger
        player.stats.AdjustHealth(-_damageAmount);
    }
}
