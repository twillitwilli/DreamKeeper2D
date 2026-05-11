using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAnimations;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputControls _inputControls;

    public bool lockMovement { get; set; }

    public float _playerSpeed { get; private set; }

    [SerializeField] LayerMask _ignoreLayers;

    [SerializeField]
    PlayerAnimations _playerAnimations;

    PlayerController _playerController;

    PlayerAnimations.FacingDirection previousFacingDirection = FacingDirection.N;

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
        Vector2 movementInput = Get8Direction(_inputControls.movement);

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

    Vector2 Get8Direction(Vector2 input)
    {
        // player not moving
        if (input == Vector2.zero)
            return Vector2.zero;

        // normalize the input so all directions have consistent magnitude
        input.Normalize();

        // gets the angle of the input direction
        // atan2 returns radians, to convert to degrees
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;

        // Snap to nearest 45 degrees
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;

        // Sets the current animation state direction
        _playerAnimations.currentFacingDirection = WhichDirection((int)snappedAngle);

        // Changes animation
        if (_playerAnimations.currentFacingDirection != previousFacingDirection)
        {
            _playerAnimations.SetAnimation();
            previousFacingDirection = _playerAnimations.currentFacingDirection;
        }

        // convert the snapped angle back to radians
        float rad = snappedAngle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    PlayerAnimations.FacingDirection WhichDirection(int snappedAngle)
    {
        switch (snappedAngle)
        {
            case 0: return FacingDirection.E;
            case 45: return FacingDirection.NE;
            case 90: return FacingDirection.N;
            case 135: return FacingDirection.NW;
            case 180:
            case -180: return FacingDirection.W;
            case -135: return FacingDirection.SW;
            case -90: return FacingDirection.S;
            case -45: return FacingDirection.SE;
        }

        return _playerAnimations.currentFacingDirection;
    }

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
