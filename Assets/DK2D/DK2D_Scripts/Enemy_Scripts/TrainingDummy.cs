using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    PlayerController _player;

    private void Start()
    {
        // gets reference to player
        _player = PlayerController.Instance;
    }

    public void HitDummy(float damageHit)
    {
        Debug.Log("Damage to Dummy = " + damageHit);

        // increase player strength exp by 1
        _player.stats.StrengthExpIncrease(1);
    }
}
