using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    EnemyController _enemy;

    public void Attack()
    {
        Debug.Log("Enemy Attacked");

        _enemy.AttackDone();
    }
}
