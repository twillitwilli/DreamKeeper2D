using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Can hit breakable objects
        BreakableObject obj;
        if (collision.TryGetComponent<BreakableObject>(out obj))
        {
            // apply damage to object
            obj.DamageObject(GetSwordDamage());
        }

        TrainingDummy dummy;
        if (collision.TryGetComponent<TrainingDummy>(out dummy))
        {
            // hit training dummy
            dummy.HitDummy(GetSwordDamage());
        }
    }

    public void DisableSword()
    {
        // disable sword after attack
        gameObject.SetActive(false);
    }

    float GetSwordDamage()
    {
        // sword damage + strength level = total damage
        float overallDamage = (damage + (damage * (_player.stats.playerStats.strength / 10)));

        Debug.Log("Overall Sword Damage = " + overallDamage);

        // return overall damage amount
        return overallDamage;
    }
}
