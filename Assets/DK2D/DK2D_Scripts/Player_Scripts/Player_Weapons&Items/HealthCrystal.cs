using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrystal : PlayerEnterExitTrigger
{
    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // Upgrades the players max health
        player.stats.MaxHealthUpgrade();

        // Destroys object after the player gets the upgrade
        Destroy(gameObject);
    }
}
