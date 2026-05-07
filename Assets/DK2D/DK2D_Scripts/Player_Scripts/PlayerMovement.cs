using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputControls _inputControls;

    public bool lockMovement { get; set; }

    public float _playerSpeed { get; private set; }

    [SerializeField] LayerMask _ignoreLayers;

    PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();

        SetPlayerSpeed();
    }

    private void Update()
    {
        if (!lockMovement)
            Movement();
    }

    void Movement()
    {
        // Movement input from the input controls
        Vector2 movementInput = _inputControls.movement;

        // Player Not Moving
        if (movementInput == Vector2.zero)
            return;

        if (!CanMove(movementInput))
            return;

        _playerController.stats.AgilityExpIncrease();

        float movementSpeed = (_playerSpeed * _playerController.stats.playerStats.agility);

        // Horizontal Movement
        float xMovement = transform.position.x + (_playerSpeed * movementInput.x * Time.deltaTime);

        // Vertical Movement
        float yMovement = transform.position.y + (_playerSpeed * movementInput.y * Time.deltaTime);

        // Final movement position of player after horizontal and vertical movements
        transform.position = new Vector3(xMovement, yMovement, 0);

        // Player Orientation
        if (movementInput != Vector2.zero)
            transform.up = movementInput;
    }

    bool CanMove(Vector2 direction)
    {
        // Can be used to visually see the raycast
        //Debug.DrawRay(transform.position, direction * 0.6f, Color.red);

        // Inverts the Layer Masks selected to ignore the selected Layers
        int layersToIgnore = ~_ignoreLayers;

        // Casts the Raycast, from the position, in the direction, with the length, and ignore the selected layers
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.6f, layersToIgnore);

        // If the raycast hits a collider, it will disable the players movement
        if (hit.collider != null && hit.collider.gameObject.tag == "Wall")
            return false;

        // If the raycast doesnt hit anything, it wil let the player continue to move
        else return true;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.6f);
    //}

    public void SetPlayerSpeed()
    {
        // Gets Player Agility Level
        float playerAgility = _playerController.stats.playerStats.agility;

        // Scales Player Speed Based On Agility Level, Max Level is 100
        float speedScale = (playerAgility - 1f) / 99f;

        // Default Player Speed 3, Max Player Speed 15
        // Clamps the Max Speed to limit max speed
        _playerSpeed = Mathf.Lerp(3f, 15f, Mathf.Clamp01(speedScale));
    }
}
