using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SoT.Player
{
    public class InputControls : MonoBehaviour
    {
        public PlayerControls controls;

        public void Awake()
        {
            // creates the reference for the Input Action
            controls = new PlayerControls();

            controls.Player_Inputs.Interact.performed += ctx => Interact();
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

        void Interact()
        {
            Debug.Log("Player Interaction");
        }
    }
}
