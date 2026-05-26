using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isDead {  get; private set; }

    public float
        health,
        maxHealth,
        shield;

    [SerializeField]
    SpriteRenderer _spriteRenderer;

    Color _defaultColor;

    private void Start()
    {
        // sets default sprite color
        _defaultColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        // fills health
        health = maxHealth;
    }

    public async void AdjustHealth(float adjustmentValue)
    {
        // checks to make sure object is not dead
        if (!isDead)
        {
            // Damage Taken
            if (adjustmentValue < 0)
            {
                // Changes color of player sprite red when hurt
                _spriteRenderer.color = Color.red;

                Debug.Log(gameObject + " Damaged");

                // Damage
                health += adjustmentValue;

                // If Health falls to zero, object dies
                if (health <= 0)
                {
                    Death();
                }

                else
                {
                    // delay before returning to default color
                    await Task.Delay(100);

                    _spriteRenderer.color = _defaultColor;
                }
            }

            // Heal
            else
            {
                Debug.Log("Player Healed");

                // Heal Health
                health += adjustmentValue;

                // Limit Current Health
                if (health > maxHealth)
                    health = maxHealth;
            }
        }

        void Death()
        {

        }
    }
}
