using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    float _health;

    public void DamageObject(float damageAmount)
    {
        // reduces health by the damage amount
        _health -= damageAmount;

        // if health is 0, break object
        if (_health <= 0)
            BreakObject();
    }

    void BreakObject()
    {
        // destroys object
        Destroy(gameObject);
    }
}
