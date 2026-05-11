using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.Interfaces;

public class FireDamageToPlayer : DamagePlayerTrigger, iCooldownable
{
    PlayerController _player;

    public float cooldownTimer { get; set; }

    public override void PlayerEnteredTrigger(PlayerController player)
    {
        // sets player reference
        _player = player;
        base.PlayerEnteredTrigger(player);
    }

    public override void PlayerExitedTrigger(PlayerController player)
    {
        // removes player refernce
        _player = null;
        base.PlayerExitedTrigger(player);
    }

    public void Update()
    {
        // checks to see if there is a player reference
        if (_player != null && CooldownDone())
        {
            // sets cooldown of fire burn overtime
            CooldownDone(true, 0.75f);

            // damages player if the player still in trigger when cooldown timer expires
            _player.stats.AdjustHealth(-_damageAmount);
        }
    }

    public bool CooldownDone(bool setTimer = false, float cooldownTime = 3)
    {
        // sets cooldown tim
        if (setTimer)
            cooldownTimer = cooldownTime;

        // if cooldown timer is still above 0, it will be reduced by time passed
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        // if cooldown timer is less than 0, return true
        else return true;

        // if cooldown timer is above 0, return false
        return false;
    }
}
