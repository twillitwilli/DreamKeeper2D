using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
    public bool CanInteract {  get; private set; }
    public TreasureChest Chest {  get; private set; }
    public NPC interactableNPC { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TreasureChest chest;
        if (collision.TryGetComponent<TreasureChest>(out chest))
        {
            // sets reference to chest if one if found
            Chest = chest;

            // sets can interact trigger on
            CanInteract = true;
        }

        NPC npc;
        if (collision.TryGetComponent<NPC>(out npc))
        {
            // sets reference to NPC
            interactableNPC = npc;

            // sets can interact trigger on
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TreasureChest chest;
        if (Chest != null && collision.TryGetComponent<TreasureChest>(out chest))
        {
            // Clears the trigger data 
            ClearTriggerData();
        }

        NPC npc;
        if (interactableNPC != null && collision.TryGetComponent<NPC>(out npc))
        {
            // Clears the trigger data
            ClearTriggerData();
        }
    }

    public void Interact()
    {
        if (Chest !=null)
        {
            // Opens chest
            Chest.OpenChest();

            // Clear trigger data
            ClearTriggerData();
        }

        else if (interactableNPC != null)
        {
            // Talk to NPC
            interactableNPC.InteractWithNPC();
        }
    }

    void ClearTriggerData()
    {
        // cant interact with anything
        CanInteract = false;

        // clears chest data from trigger
        Chest = null;

        // clears npc data from trigger
        interactableNPC = null;
    }
}
