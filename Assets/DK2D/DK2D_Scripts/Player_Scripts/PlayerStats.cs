using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    float _healthCap = 10000;

    int _goldCap = 10000;

    float
        _playerStep,
        _agilityCap = 100,
        _agilityNextLevelUp,
        _strengthCap = 100,
        _magicCap = 10000,
        _intelligenceCap = 100;

    public PlayerStatsData playerStats;

    private void Start()
    {
        SetAgilityLevelUpExpReq();
    }

    // ===================== HEALTH STAT ======================

    public void AdjustHealth(float adjustmentValue)
    {
        // Damage Taken
        if (adjustmentValue < 0)
        {
            Debug.Log("Player Damaged");

            // Damage Player
            playerStats.health += adjustmentValue;

            // If Player Health falls to zero, player dies
            if (playerStats.health <= 0)
            {
                Death();
            }
        }

        // Heal
        else
        {
            Debug.Log("Player Healed");

            // Heal Player Health
            playerStats.health += adjustmentValue;

            // Limit Player Current Health
            if (playerStats.health > playerStats.maxHealth)
                playerStats.health = playerStats.maxHealth;
        }
    }

    void Death()
    {
        Debug.Log("Player Died");
    }

    public void MaxHealthUpgrade()
    {
        Debug.Log("Player Got Max Health Upgrade");

        // Increase Player Max Health
        playerStats.maxHealth += 250;

        // Limit Player Max Health
        if (playerStats.maxHealth > _healthCap)
            playerStats.maxHealth = _healthCap;
    }

    // ========================================================

    // ======================= GOLD ===========================

    public void GainedGold(int goldGained)
    {
        // Increases player current gold amount
        playerStats.gold += goldGained;

        // Limit Player Current Gold
        if (playerStats.gold > playerStats.maxGold)
            playerStats.gold = playerStats.maxGold;
    }

    public void GoldUsed(int goldPrice)
    {
        // Reduces amount of gold by gold price
        playerStats.gold -= goldPrice;

        // Limits gold amount to 0
        if (playerStats.gold <= 0)
            playerStats.gold = 0;
    }

    public void MaxGoldUpgrade()
    {
        // Increase Player Max Gold
        playerStats.maxGold += 500;

        // Limit Player Max Gold 
        if (playerStats.maxGold > _goldCap)
            playerStats.maxGold = _goldCap;
    }

    // ========================================================

    // ===================== AGILITY STAT =====================

    public void AgilityExpIncrease()
    {
        // If Agility Is Not At Max Level
        if (playerStats.agility < 100)
        {
            // Adds 1 Step Per Player Movement
            _playerStep++;

            // 350 steps equals 1 exp
            if (_playerStep >= 350)
            {
                // resets player steps to gain 1 more exp
                _playerStep = 0;

                // increases player agility exp by +1
                playerStats.agilityExp++;
            }
            // if player has not made 350 steps, stops here
            else return;

            // Checks to see if the players current agility exp reaches agility level up
            if (playerStats.agilityExp >= _agilityNextLevelUp)
            {
                Debug.Log("Agility Level Up");

                // resets player agility exp
                playerStats.agilityExp = 0;

                // increases players agility level by 1
                playerStats.agility++;

                // increases players movement speed
                _player.movement.SetPlayerSpeed();

                // increase the exp requirement needed to for next level
                SetAgilityLevelUpExpReq();

                // Limits players max agility level too 100
                if (playerStats.agility >= _agilityCap)
                    playerStats.agility = 100;
            }
        }
    }

    void SetAgilityLevelUpExpReq()
    {
        // scales how much exp is needed for next level up
        _agilityNextLevelUp = 100 * Mathf.Pow(playerStats.agility, 2);
    }

    // ========================================================

    // ================== STRENGTH STAT =======================

    // ========================================================
}
