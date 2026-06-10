using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool disableEnemy { get; set; }

    [SerializeField]
    bool _isBoss;

    public enum EnemyState
    {
        idle,
        chasing,
        attacking,
        wandering,
        teleporting
    }

    public EnemyState currentState;

    public Health health;

    [SerializeField]
    EnemyAttack _enemyAttack;

    public float
        movementSpeed,
        sightRange,
        attackRange,
        attackDamage,
        attackCooldown;

    public int pointsPerKill = 1;

    bool _isAttacking;
    float _cdTimer;

    public PlayerController player { get; set; }

    private void Start()
    {
        // gets reference to player
        player = PlayerController.Instance;
    }

    private void OnEnable()
    {
        // restores enemy health
        health.AdjustHealth(9999999999);

        // enemy disabled false
        disableEnemy = false;
    }

    private void Update()
    {
        // when game is pause
        if (Time.timeScale == 0)
            return;

        if (!disableEnemy)
        {
            // Gets distance from player
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

            // if enemy is not attacking
            if (!_isAttacking)
            {
                // checks to see if enemy is within range to attack
                if (AttackCD() && distanceFromPlayer < attackRange)
                    _isAttacking = true;

                // Enemy Is Moving
                else Movement(distanceFromPlayer);
            }

            // enemy is attacking
            else
            {
                // if enemy has an attack, then enemy will attack
                if (_enemyAttack != null)
                    _enemyAttack.Attack();

                // enemy has no attacks to attack with
                else _isAttacking = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if player collides with enemy, player will get hurt
        PlayerController playerCheck;
        if (collision.gameObject.TryGetComponent<PlayerController>(out playerCheck))
        {
            player.playerCamera.ScreenShake();
            player.stats.AdjustHealth(-150);
        }
    }

    private void Movement(float distanceFromPlayer)
    {
        // if player is within enemy sight range, the enemy will start chasing player
        if (distanceFromPlayer < sightRange)
            currentState = EnemyState.chasing;

        // else enemy is in idle state
        else currentState = EnemyState.idle;

            switch (currentState)
            {
                case EnemyState.chasing:

                    ChasePlayer();

                    break;

                case EnemyState.wandering:

                    break;

                case EnemyState.teleporting:

                    break;
            }
    }

    public virtual void AimAtTarget(Transform target)
    {
        // makes the enemy look in the player direction
        transform.up = transform.position - target.position;
    }

    private void ChasePlayer()
    {
        // looks at player
        AimAtTarget(player.transform);

        // moves enemy in direction of the player
        transform.position = Vector3.Lerp(transform.position, player.transform.position, (0.005f * movementSpeed));
    }

    public void AttackDone()
    {
        // sets attack cooldown
        AttackCD(true);

        // sets is attacking to false
        _isAttacking = false;
    }

    bool AttackCD(bool startCooldown = false)
    {
        // Start cooldown if requested
        if (startCooldown)
        {
            _cdTimer = attackCooldown;

            // immediately return false since cooldown just started
            return false;
        }

        // if cooldown is active, count down
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
            return false;
        }

        // Cooldown is finished
        return true;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draws attack range
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackRange);
#endif

#if UNITY_EDITOR
        // draws sight range
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, sightRange);
#endif
    }
}
