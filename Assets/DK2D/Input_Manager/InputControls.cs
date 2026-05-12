using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    UI_Manager _UIManager;

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

        // Toggle Menu Controller
        controls.Player_Inputs.Menu.performed += ctx => ToggleMenu();
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

    private void Start()
    {
        _UIManager = UI_Manager.Instance;
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

    async void ToggleMenu()
    {
        // Toggles Menu On and Pauses Game
        if (!_UIManager.menu.activeSelf)
        {
            _UIManager.menu.SetActive(true);

            // small delay before updating displays
            await Task.Delay(10);

            // Refreshes all UI Displays
            _player.stats.UpdateAllDisplays();

            Debug.Log("Pause Game");
        }

        // Toggles Menu Off and Unpauses Game
        else
        {
            _UIManager.menu.SetActive(false);

            Debug.Log("Unpause Game");
        }
    }
}
