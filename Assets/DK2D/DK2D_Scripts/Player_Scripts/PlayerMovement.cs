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

        float _playerSpeed = 1f;

        private void Update()
        {
            Movement();
        }

        void Movement()
        {
            // Horizontal Movement
            float xMovement = transform.position.x + (_playerSpeed * _inputControls.movement.x * Time.deltaTime);

            // Vertical Movement
            float yMovement = transform.position.y + (_playerSpeed * _inputControls.movement.y * Time.deltaTime);

            // Final movement position of player after horizontal and vertical movements
            transform.position = new Vector3(xMovement, yMovement, 0);
        }
    }
}
