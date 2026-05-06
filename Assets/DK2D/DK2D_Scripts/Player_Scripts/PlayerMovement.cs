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

        public bool lockMovement { get; set; }
        float _playerSpeed = 6f;

        [SerializeField] LayerMask _ignoreLayers;

        private void Update()
        {
            if (!lockMovement)
                Movement();
        }

        void Movement()
        {
            // Movement input from the input controls
            Vector2 movementInput = _inputControls.movement;

            if (!CanMove(movementInput))
                return;

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
    }
}
