using System;
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

    bool[] _attacks;

    [SerializeField]
    PlayerController _player;

    public float damage;

    Animator _animator;

    private void Start()
    {
        // sets animator reference
        _animator = GetComponent<Animator>();

        // sets the amount of bools based on how many attacks there is
        _attacks = new bool[Enum.GetValues(typeof(SwordAttacks)).Length];
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

    // Triggered at end of animation
    public void DisableSword()
    {
        // when sword is disabled will reset all attack animations
        for (int i = 0; i < _attacks.Length; i++)
            _attacks[i] = false;

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
        // switches between which attack is being performed
        switch (currentSwordAttack)
        {
            case SwordAttacks.basic:
                AttackAnimation(0, "SwordSlashBasic");
                break;

            case SwordAttacks.doubleSlash:
                AttackAnimation(1, "SwordDoubleSlash");
                break;

            case SwordAttacks.swordSpin:
                AttackAnimation(2, "SwordSpin");
                break;

            case SwordAttacks.groundSlam:
                AttackAnimation(3, "GroundSlam");
                break;
        }

    }

    void AttackAnimation(int whichAttack, string attackName)
    {
        // if attack has not already started, will start attack
        if (!_attacks[whichAttack])
        {
            // plays attack animation
            _animator.Play(attackName);

            // sets this attack to playing already so it cant be triggered twice
            _attacks[whichAttack] = true;
        }
    }
}
