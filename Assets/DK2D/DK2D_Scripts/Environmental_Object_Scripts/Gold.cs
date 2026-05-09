using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : PlayerEnterExitTrigger
{
    [SerializeField]
    int _goldAmount;

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // adds gold to the gold gained
        player.stats.GainedGold(_goldAmount);

        // Destroys object after the player gets the gold
        Destroy(gameObject);
    }
}
