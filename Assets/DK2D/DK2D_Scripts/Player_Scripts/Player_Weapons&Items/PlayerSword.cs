using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public enum SwordAttacks
    {
        basic,
        doubleSlash,
        swordSpin,
        groundSlam
    }

    public SwordAttacks currentSwordAttack;

    [SerializeField]
    PlayerController _player;

    public float damage;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrainingDummy dummy;
        if (collision.TryGetComponent<TrainingDummy>(out dummy))
        {
            // hit training dummy
            dummy.HitDummy(GetSwordDamage());
        }

        Health health;
        if (collision.TryGetComponent<Health>(out health))
        {
            health.AdjustHealth(GetSwordDamage());
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

    public void SwordAttack()
    {
        switch (currentSwordAttack)
        {
            case SwordAttacks.basic:
                _animator.Play("SwordSlashBasic");
                break;

            case SwordAttacks.doubleSlash:
                _animator.Play("SwordDoubleSlash");
                break;

            case SwordAttacks.swordSpin:
                _animator.Play("SwordSpin");
                break;

            case SwordAttacks.groundSlam:
                _animator.Play("GroundSlam");
                break;
        }

    }
}
