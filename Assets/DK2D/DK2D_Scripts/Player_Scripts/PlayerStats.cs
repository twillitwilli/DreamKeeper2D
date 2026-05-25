using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameManager _gameManager;

    bool _playedDead;

    [SerializeField]
    PlayerController _player;

    [SerializeField]
    SpriteRenderer _playerSprite;

    [SerializeField]
    UI_Manager _UIManager;

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

    private void Start()
    {
        _gameManager = GameManager.Instance;

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

    public async void SwitchHealthDisplays()
    {
        // set health display to dream state
        if (playerStats.dreamState)
        {
            // changes color of health bar
            _UIManager.healthBarSwitch.SpiritHealthBar();

            // delay before updating further
            await Task.Delay(500);

            // update current display number
            UpdateHealthDisplay(playerStats.spiritHealth, playerStats.maxSpiritHealth);
        }

        // set health display to normal
        else
        {
            // changes color of health bar
            _UIManager.healthBarSwitch.NormalHealthBar();

            // delay before updating further
            await Task.Delay(500);

            // update current display number
            UpdateHealthDisplay(playerStats.health, playerStats.maxHealth);
        }
    }

    public async Task AdjustHealth(float adjustmentValue)
    {
        // checks to make sure player is not dead
        if (!_playedDead && !playerStats.dreamState)
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

            UpdateHealthDisplay(playerStats.health, playerStats.maxHealth);
        }

        // ----------------- Spirit Health Adjustments -------------------

        else if (playerStats.dreamState)
        {
            // Damage Taken
            if (adjustmentValue < 0)
            {
                // Changes color of player sprite red when hurt
                _playerSprite.color = Color.red;

                Debug.Log("Player Damaged");

                // Damage Player
                playerStats.spiritHealth += adjustmentValue;

                // If Player Health falls to zero, player dies
                if (playerStats.spiritHealth <= 0)
                {
                    DreamDeath();

                    return;
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
                playerStats.spiritHealth += adjustmentValue;

                // Limit Player Current Health
                if (playerStats.spiritHealth > playerStats.maxSpiritHealth)
                    playerStats.spiritHealth = playerStats.maxSpiritHealth;
            }

            UpdateHealthDisplay(playerStats.spiritHealth, playerStats.maxSpiritHealth);
        }
    }

    void Death()
    {
        Debug.Log("Player Died");

        // sets player to dead
        _playedDead = true;

        // lock player movement
        _player.movement.lockMovement = true;

        // turn on game over screen
        GameManager.Instance.gameOver.gameObject.SetActive(true);
    }

    void DreamDeath()
    {
        Debug.Log("Player Died In Dream & Wakes Up");

        // locks player movement
        _player.movement.lockMovement = true;

        //change scene and wake player up
        playerStats.dreamState = false;
        GameManager.Instance.ChangePlayerScene();
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

    public void MaxSpiritHealthUpgrade()
    {
        Debug.Log("Player Got Max Health Upgrade");

        // Increase Player Max Health
        playerStats.maxSpiritHealth += 250;

        // Limit Player Max Health
        if (playerStats.maxSpiritHealth > _healthCap)
            playerStats.maxSpiritHealth = _healthCap;

        AdjustHealth(250);
    }

    void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        // gets current health percent
        float currentHealthPercent = 100 * (currentHealth / maxHealth);

        // sets current value to int
        int currentHealthInt = (int)currentHealth;
        // sets max value to int
        int currentMaxHealthInt = (int)maxHealth;

        // string display for the text display
        string healthDisplay = currentHealthInt + "/" + currentMaxHealthInt;

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
        if (!playerStats.dreamState && playerStats.agility < 100)
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
                AgilityLevelUp();
            }
        }

    }

    void AgilityLevelUp()
    {
        Debug.Log("Agility Level Up");

        // spawns in agility level up indicator
        Instantiate(_gameManager.levelUpIndicators.prefabs[0]);

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
        if (!playerStats.dreamState &&  playerStats.strength < 100)
        {
            // add exp gained to strength exp
            playerStats.strengthExp += exp;

            // if strength exp equals next level up requirement, level up strength
            if (playerStats.strengthExp >= _strengthNextLevelUp)
            {
                StrengthLevelUp();
            }
        }
    }

    void StrengthLevelUp()
    {
        Debug.Log("Strength Level Up");

        // spawns in strength level up indicator
        Instantiate(_gameManager.levelUpIndicators.prefabs[1]);

        // extra exp variable
        float extraExp = 0;

        // get extra exp to transfer before level up
        if (playerStats.strengthExp > _strengthNextLevelUp)
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
        if (!playerStats.dreamState && playerStats.intelligence < 100)
        {
            // add exp gained to strength exp
            playerStats.intelligenceExp += exp;

            // if strength exp equals next level up requirement, level up strength
            if (playerStats.intelligenceExp >= _intelligenceNextLevelUp)
            {
                IntelligenceLevelUp();
            }
        }
    }

    void IntelligenceLevelUp()
    {
        Debug.Log("Intelligence Level Up");

        // spawns in intelligence level up indicator
        Instantiate(_gameManager.levelUpIndicators.prefabs[2]);

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
        UpdateHealthDisplay(playerStats.health, playerStats.maxHealth);
        UpdateGoldDisplay();
        UpdateAgilityDisplay();
        UpdateStrengthDisplay();
        UpdateMagicDisplay();
        UpdateIntelligenceDisplay();
    }    
}
