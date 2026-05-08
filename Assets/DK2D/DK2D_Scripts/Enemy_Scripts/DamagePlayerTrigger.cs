using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerTrigger : MonoBehaviour
{
    [SerializeField]
    float damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player;
        if (collision.TryGetComponent<PlayerController>(out player))
        {
            // damages player if the player enters the trigger
            player.stats.AdjustHealth(-damageAmount);
        }
    }
}
