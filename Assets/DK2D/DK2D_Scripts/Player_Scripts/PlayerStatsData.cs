[System.Serializable]
public class PlayerStatsData
{
    public string playerName;

    public bool dreamState;

    public float
        // Health
        health,
        maxHealth,
        spiritHealth,
        maxSpiritHealth;

    public int
        // Gold
        gold,
        maxGold;

    public float
        // Agility
        agility,
        agilityExp,

        // Strength
        strength,
        strengthExp,

        // Bow
        arrows,
        maxArrows,

        // Magic
        magic,
        maxMagic,

        // Intelligence
        intelligence,
        intelligenceExp;
}
