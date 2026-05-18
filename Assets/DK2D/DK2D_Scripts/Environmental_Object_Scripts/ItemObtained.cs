using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObtained : MonoBehaviour
{
    public enum items
    {
        heartCrystal
    }

    public items item;

    [SerializeField]
    bool gainLoot;

    public void Start()
    {
        // sets reference to item obtained in UI Manager
        UI_Manager.Instance.itemObtained = this;

        // give player loot
        if (gainLoot)
        {
            switch (item)
            {
                case items.heartCrystal:

                    PlayerController.Instance.stats.MaxHealthUpgrade();

                    break;
            }
        }
    }

    public void ClearItemDesription()
    {
        Destroy(this.gameObject);
    }
}
