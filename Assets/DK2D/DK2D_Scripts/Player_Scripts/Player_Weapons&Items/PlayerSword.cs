using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Can hit breakable objects
        BreakableObject obj;
        if (collision.TryGetComponent<BreakableObject>(out obj))
        {
            // apply damage to object
            obj.DamageObject(damage);
        }
    }

    public void DisableSword()
    {
        // disable sword after attack
        gameObject.SetActive(false);
    }
}
