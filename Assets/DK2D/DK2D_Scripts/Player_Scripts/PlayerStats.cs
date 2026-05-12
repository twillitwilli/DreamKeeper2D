using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    [SerializeField]
    SpriteRenderer _playerSprite;

    Color _playerDefaultColor;

    float _healthCap = 10000;

    int _goldCap = 10000;

    float
        _playerStep,
        _agilityCap = 100,
        _agilityNextLevelUp,
        _strengthCap = 100,
        _strengthNextLevelUp,
        _magicCap = 10000,
        _intelligenceCap = 100,
        _intelligenceNextLevelUp = 100;

    public PlayerStatsData playerStats;

    UI_Manager _UIManager;

    private void Start()
    {
        // sets players default sprite color
        _playerDefaultColor = _playerSprite.color;

        // gets reference to UI Manager
        _UIManager = UI_Manager.Instance;

        SetAgilityLevelUpExpReq();
        SetStrengthLevelUpExpReq();
        SetIntelligenceLevelUpExpReq();

        UpdateAllDisplays();
    }

    // ===================== HEALTH STAT ======================

    public async Task AdjustHealth(float adjustmentValue)
    {
        // Damage Taken
        if (adjustmentValue < 0)
        {
            // Changes color of player sprite red when hurt
            _playerSprite.color = Color.red;

            Debug.Log("Player Damaged");

            // Damage Player
            playerStats.health += adjustmentValue;

            // If Player Health falls to zero, player dies
            if (playerStats.health <= 0)
            {
                Death();
            }

            else
            {
                // delay before returning player to default color
                await Task.Delay(100);

                _playerSprite.color = _playerDefaultColor;
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

        UpdateHealthDisplay();
    }

    void Death()
    {
        Debug.Log("Player Died");

        Destroy(_player.gameObject);
    }

    public void MaxHealthUpgrade()
    {
        Debug.Log("Player Got Max Health Upgrade");

        // Increase Player Max Health
        playerStats.maxHealth += 250;

        // Limit Player Max Health
        if (playerStats.maxHealth > _healthCap)
            playerStats.maxHealth = _healthCap;

        AdjustHealth(250);
    }

    void UpdateHealthDisplay()
    {
        // gets current health percent
        float currentHealthPercent = 100 * (playerStats.health / playerStats.maxHealth);

        // sets current value to int
        int currentHealth = (int)playerStats.health;
        // sets max value to int
        int currentMaxHealth = (int)playerStats.maxHealth;

        // string display for the text display
        string healthDisplay = currentHealth + "/" + currentMaxHealth;

        _UIManager.healthBar.SetAnimationFrame(currentHealthPercent, healthDisplay);
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

        UpdateGoldDisplay();
    }

    public void GoldUsed(int goldPrice)
    {
        // Reduces amount of gold by gold price
        playerStats.gold -= goldPrice;

        // Limits gold amount to 0
        if (playerStats.gold <= 0)
            playerStats.gold = 0;

        UpdateGoldDisplay();
    }

    public void MaxGoldUpgrade()
    {
        // Increase Player Max Gold
        playerStats.maxGold += 500;

        // Limit Player Max Gold 
        if (playerStats.maxGold > _goldCap)
            playerStats.maxGold = _goldCap;
    }

    void UpdateGoldDisplay()
    {
        _UIManager.goldTextDisplay.text = playerStats.gold.ToString();
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

        UpdateAgilityDisplay();
    }

    void SetAgilityLevelUpExpReq()
    {
        // scales how much exp is needed for next level up
        _agilityNextLevelUp = 100 * Mathf.Pow(playerStats.agility, 2);
    }

    void UpdateAgilityDisplay()
    {
        // gets current percent
        int currentAgilityPercent = (int)(100 * (playerStats.agilityExp / _agilityNextLevelUp));

        // sets current value to int
        int currentExp = (int)playerStats.agilityExp;
        // sets max value to int
        int neededExp = (int)_agilityNextLevelUp;

        // string display for the text display
        string agilityDisplay = "Agility Level " + playerStats.agility.ToString() + "\n" + currentAgilityPercent.ToString() + "%";

        _UIManager.agilityBar.SetAnimationFrame(currentAgilityPercent, agilityDisplay);
    }

    // ========================================================

    // ================== STRENGTH STAT =======================

    public void StrengthExpIncrease(float exp)
    {
        // If player strength stat is not at max level
        if (playerStats.strength < 100)
        {
            // add exp gained to strength exp
            playerStats.strengthExp += exp;

            // if strength exp equals next level up requirement, level up strength
            if (playerStats.strengthExp >= _strengthNextLevelUp)
            {
                Debug.Log("Strength Level Up");

                // extra exp variable
                float extraExp = 0;

                // get extra exp to transfer before level up
                if (playerStats.strengthExp >  _strengthNextLevelUp)
                    extraExp = playerStats.strengthExp - _strengthNextLevelUp;

                // reset player strength exp
                playerStats.strengthExp = 0;

                // increase strength level by 1
                playerStats.strength++;

                // increase requirement for next level up
                SetStrengthLevelUpExpReq();

                // transfer remaining exp from previous level to new level
                playerStats.strengthExp += extraExp;

                // limits player max stregth level
                if (playerStats.strength >= _strengthCap)
                    playerStats.strength = 100;
            }
        }

        UpdateStrengthDisplay();
    }

    void SetStrengthLevelUpExpReq()
    {
        // scales how much exp is needed for next level up
        _strengthNextLevelUp = 25 * Mathf.Pow(playerStats.strength, 2);
    }

    void UpdateStrengthDisplay()
    {
        // gets current percent
        int currentStrengthPercent = (int)(100 * (playerStats.strengthExp / _strengthNextLevelUp));

        // sets current value to int
        int currentExp = (int)playerStats.strengthExp;
        // sets max value to int
        int neededExp = (int)_strengthNextLevelUp;

        // string display for the text display
        string strengthDisplay = "Strength Level " + playerStats.strength + "\n" + currentStrengthPercent + "%";

        _UIManager.strengthBar.SetAnimationFrame(currentStrengthPercent, strengthDisplay);
    }

    // ========================================================

    // ==================== MAGIC STAT ========================

    void UpdateMagicDisplay()
    {
        // gets current percent
        float currentMagicPercent = 100 * (playerStats.magic / playerStats.maxMagic);

        // sets current value to int
        int currentMagic = (int)playerStats.magic;
        // sets max value to int
        int maxMagic = (int)playerStats.maxMagic;

        // string display for the text display
        string magicDisplay = currentMagic + "/" + maxMagic;

        _UIManager.magicBar.SetAnimationFrame(currentMagicPercent, magicDisplay);
    }

    // ========================================================

    // ================ INTELLIGENCE STAT =====================

    public void IntelligenceExpIncrease(float exp)
    {
        // If player strength stat is not at max level
        if (playerStats.intelligence < 100)
        {
            // add exp gained to strength exp
            playerStats.intelligenceExp += exp;

            // if strength exp equals next level up requirement, level up strength
            if (playerStats.intelligenceExp >= _intelligenceNextLevelUp)
            {
                Debug.Log("Intelligence Level Up");

                // extra exp variable
                float extraExp = 0;

                // get extra exp to transfer before level up
                if (playerStats.intelligenceExp > _intelligenceNextLevelUp)
                    extraExp = playerStats.intelligenceExp - _intelligenceNextLevelUp;

                // reset player strength exp
                playerStats.intelligenceExp = 0;

                // increase strength level by 1
                playerStats.intelligence++;

                // increase requirement for next level up
                SetIntelligenceLevelUpExpReq();

                // transfer remaining exp from previous level to new level
                playerStats.intelligenceExp += extraExp;

                // limits player max stregth level
                if (playerStats.intelligence >= _intelligenceCap)
                    playerStats.intelligence = 100;
            }
        }

        UpdateIntelligenceDisplay();
    }

    void SetIntelligenceLevelUpExpReq()
    {
        // scales how much exp is needed for next level up
        _intelligenceNextLevelUp = 25 * Mathf.Pow(playerStats.intelligence, 2);
    }

    void UpdateIntelligenceDisplay()
    {
        // gets current percent
        int currentIntelligencePercent = (int)(100 * (playerStats.intelligenceExp / _intelligenceNextLevelUp));

        // sets current value to int
        int currentExp = (int)playerStats.intelligenceExp;
        // sets max value to int
        int neededExp = (int)_intelligenceNextLevelUp;

        // string display for the text display
        string intelligenceDisplay = "Intelligence Level " + playerStats.intelligence + "\n" + currentIntelligencePercent + "%";

        _UIManager.intelligenceBar.SetAnimationFrame(currentIntelligencePercent, intelligenceDisplay);
    }

    // ========================================================

    public void UpdateAllDisplays()
    {
        UpdateHealthDisplay();
        UpdateGoldDisplay();
        UpdateAgilityDisplay();
        UpdateStrengthDisplay();
        UpdateMagicDisplay();
        UpdateIntelligenceDisplay();
    }    
}
