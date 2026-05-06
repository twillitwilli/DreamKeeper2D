using SoT.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace SoT.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputControls _inputControls;

        float _playerSpeed = 6f;

        private void Update()
        {
            Movement();
        }

        void Movement()
        {
            // Movement input from the input controls
            Vector2 movementInput = _inputControls.movement;

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
    }
}
