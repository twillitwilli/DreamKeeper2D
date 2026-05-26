using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoT.AbstractClasses;

public class PlayerController : MonoSingleton<PlayerController>
{
    public PlayerMovement movement;
    public PlayerStats stats;
    public PlayerInteractionTrigger interactionTrigger;
    public PlayerCamera playerCamera;
}
