using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField]
    bool _guaranteedLoot;

    [Range(0f, 100f)]
    public float dropPercentage;

    [SerializeField]
    GameObject _lootPrefab;

    private void OnDestroy()
    {
        // Guranteed Loot
        if (_guaranteedLoot)
            DropLoot();

        // Random Drop Chance
        else
        {
            // Drop chance
            float dropChance = Random.Range(0,100);

            // If drop chance is less than drop percentage, will drop
            if (dropChance < dropPercentage)
                DropLoot();
        }
    }

    void DropLoot()
    {
        // spawn new loot prefab object at the location of the destroyed object
        GameObject newLootObject = Instantiate(_lootPrefab, transform.position, transform.rotation);
    }
}
