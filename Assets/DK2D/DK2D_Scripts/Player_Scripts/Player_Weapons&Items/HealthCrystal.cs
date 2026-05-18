using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrystal : PlayerEnterExitTrigger
{
    [SerializeField]
    GameObject itemObtainedDescription;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // displays item description
        Instantiate(itemObtainedDescription, transform.position, transform.rotation);

        // Upgrades the players max health
        player.stats.MaxHealthUpgrade();

        // Destroys object after the player gets the upgrade
        Destroy(gameObject);
    }
}
