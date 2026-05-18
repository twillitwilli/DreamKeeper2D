using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
    PlayerController _player;

    public enum Interactables
    {
        none,
        chest,
        NPC,
        door
    }

    public Interactables currentInteractable;

    public TreasureChest Chest {  get; private set; }
    public NPC interactableNPC { get; private set; }
    public Door interactableDoor {  get; private set; }

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TreasureChest chest;
        if (collision.TryGetComponent<TreasureChest>(out chest))
        {
            // sets reference to chest if one if found
            Chest = chest;

            // sets current interactable to chest
            currentInteractable = Interactables.chest;
        }

        NPC npc;
        if (collision.TryGetComponent<NPC>(out npc))
        {
            // sets reference to NPC
            interactableNPC = npc;

            // sets current interactable to NPC
            currentInteractable = Interactables.NPC;
        }

        Door door;
        if (collision.TryGetComponent<Door>(out door))
        {
            // sets reference to door
            door = interactableDoor;

            // sets current interactable to door
            currentInteractable = Interactables.door;
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

        Door door;
        if (interactableDoor != null && collision.TryGetComponent<Door>(out door))
        {
            // Clears the trigger data
            ClearTriggerData();
        }
    }

    public void Interact()
    {
        switch (currentInteractable)
        {
            case Interactables.chest:

                // Opens chest
                Chest.OpenChest();

                // Clear trigger data
                ClearTriggerData();

                break;

            case Interactables.NPC:

                // Talk to NPC
                interactableNPC.InteractWithNPC();

                break;

            case Interactables.door:

                // Opens door
                interactableDoor.OpenDoor(_player);

                // Clear trigger data
                ClearTriggerData();

                break;
        }
    }

    void ClearTriggerData()
    {
        // sets current interactable to none
        currentInteractable = Interactables.none;

        // clears chest data from trigger
        Chest = null;

        // clears npc data from trigger
        interactableNPC = null;

        // clears door data from trigger
        interactableDoor = null;
    }
}
