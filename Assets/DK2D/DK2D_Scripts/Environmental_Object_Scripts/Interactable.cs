using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum Interactables
    {
        none,
        chest,
        NPC,
        door,
        bed
    }

    public Interactables typeOfInteraction;

    public TreasureChest chest { get; private set; }
    public NPC interactableNPC { get; private set; }
    public Door interactableDoor { get; private set; }
    public Bed interactableBed { get; private set; }

    private void Start()
    {
        switch (typeOfInteraction)
        {
            case Interactables.none:
                Debug.Log("No Interaction Found");
                break;

            case Interactables.chest:
                // sets reference to chest
                chest = GetComponent<TreasureChest>();
                break;

            case Interactables.NPC:
                // sets reference to NPC
                interactableNPC = GetComponent<NPC>();
                break;

            case Interactables.door:
                // sets reference to door
                interactableDoor = GetComponent<Door>();
                break;

            case Interactables.bed:
                // sets reference to bed
                interactableBed = GetComponent<Bed>();
                break;
        }
    }

    public void Interact(PlayerController player)
    {
        switch (typeOfInteraction)
        {
            case Interactables.none:
                Debug.Log("No Interaction Found");
                break;

            case Interactables.chest:
                // interacts with chest
                ChestInteraction();
                break;

            case Interactables.NPC:
                // interacts with npc
                NPCInteraction();
                break;

            case Interactables.door:
                // interacts with door
                DoorInteraction(player);
                break;

            case Interactables.bed:
                // interacts with bed
                BedInteraction(player);
                break;
        }
    }

    void ChestInteraction()
    {
        // opens chest
        chest.OpenChest();
    }

    void NPCInteraction()
    {
        interactableNPC.InteractWithNPC();
    }

    void DoorInteraction(PlayerController player)
    {
        interactableDoor.OpenDoor(player);
    }

    void BedInteraction(PlayerController player)
    {
        interactableBed.SleepInBed(player);
    }
}
