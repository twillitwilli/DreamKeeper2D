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

    GameManager _gameManager;
    UI_Manager _UIManager;
    PlayerSword _sword;

    public void Awake()
    {
        // set reference to sword
        _sword = _playerSword.GetComponent<PlayerSword>();

        // creates the reference for the Input Action
        controls = new PlayerControls();

        // Interaction controller (when interaction is performed, will trigger the Interaction function)
        controls.Player_Inputs.Interact.performed += ctx => Interact();

        // Movement controller
        controls.Player_Inputs.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player_Inputs.Movement.canceled += ctx => movement = new Vector2(0, 0);

        // Sword Attacks controller //
        controls.Player_Inputs.SwordAttack.performed += ctx => SwordAttack();

        controls.Player_Inputs.DoubleSlash.performed += ctx => DoubleSlash();

        controls.Player_Inputs.SwordSpin.performed += ctx => SwordSpin();

        controls.Player_Inputs.GroundSlam.performed += ctx => GroundSlam();

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
        _gameManager = GameManager.Instance;
        _UIManager = UI_Manager.Instance;
    }

    private void Update()
    {
        //Debug.Log("Movement Vector = " + movement);
    }

    void Interact()
    {
        Debug.Log("Player Interaction");

        // if game over screen active
        if (_gameManager.gameOver.activeScreen)
            _gameManager.GameOver();

        // if item obtained description is open will clear item desription
        else if (_UIManager.itemObtained != null)
            _UIManager.itemObtained.ClearItemDesription();

        // checks if player interaction trigger can interact with something
        else if (_player.interactionTrigger.currentInteractable != null)
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

        // set sword attack
        _sword.currentSwordAttack = PlayerSword.SwordAttacks.basic;

        // use sword attack
        _sword.SwordAttack();
    }

    void DoubleSlash()
    {
        if (!_playerSword.activeSelf)
            _playerSword.SetActive(true);

        // set sword attack
        _sword.currentSwordAttack = PlayerSword.SwordAttacks.doubleSlash;

        // use sword attack
        _sword.SwordAttack();
    }

    void SwordSpin()
    {
        if (!_playerSword.activeSelf)
            _playerSword.SetActive(true);

        // set sword attack
        _sword.currentSwordAttack = PlayerSword.SwordAttacks.swordSpin;

        // use sword attack
        _sword.SwordAttack();
    }

    void GroundSlam()
    {
        if (!_playerSword.activeSelf)
            _playerSword.SetActive(true);

        // set sword attack
        _sword.currentSwordAttack = PlayerSword.SwordAttacks.groundSlam;

        // use sword attack
        _sword.SwordAttack();
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
            // locks player movement while menu open
            _player.movement.lockMovement = true;

            // opens menu
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
            // closes menu
            _UIManager.menu.SetActive(false);

            // unlock player movement
            _player.movement.lockMovement = false;

            Debug.Log("Unpause Game");
        }
    }
}
