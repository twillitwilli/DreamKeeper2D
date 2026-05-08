using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControls : MonoBehaviour
{
    public PlayerControls controls;

    public Vector2 movement { get; private set; }

    [SerializeField]
    PlayerController _player;

    [SerializeField]
    GameObject _playerSword;

    public void Awake()
    {
        // creates the reference for the Input Action
        controls = new PlayerControls();

        // Interaction controller (when interaction is performed, will trigger the Interaction function)
        controls.Player_Inputs.Interact.performed += ctx => Interact();

        // Movement controller
        controls.Player_Inputs.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player_Inputs.Movement.canceled += ctx => movement = new Vector2(0, 0);

        // Sword Attack controller
        controls.Player_Inputs.SwordAttack.performed += ctx => SwordAttack();

        // Use Item controller
        controls.Player_Inputs.UseItem.performed += ctx => UseItem();
    }

    private void OnEnable()
    {
        // enables the controls when this object is enabled
        controls.Enable();
    }

    private void OnDisable()
    {
        // disables the controls when this object is disabled
        controls.Disable();
    }

    private void Update()
    {
        //Debug.Log("Movement Vector = " + movement);
    }

    void Interact()
    {
        Debug.Log("Player Interaction");

        // checks if player interaction trigger can interact with something
        if (_player.interactionTrigger.CanInteract)
        {
            // player interacts with interactable
            _player.interactionTrigger.Interact();
        }
    }

    void SwordAttack()
    {
        // if sword isnt active, activates sword
        if (!_playerSword.activeSelf)
            _playerSword.SetActive(true);
    }

    void UseItem()
    {
        Debug.Log("Use Item");
    }
}
