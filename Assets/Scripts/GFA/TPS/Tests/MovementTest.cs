using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFA.TPS.Movement.Tests
{

    public class MovementTest : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _movementInput;

        [SerializeField]
        private CharacterMovement _characterMovement;

        [SerializeField]
        private Vector3 _externalForceValue;

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
        }

        private void OnEnable()
        {
            _gameInput.Enable();

            _gameInput.Player.Dodge.performed += OnDodgeButtonPressed;
        }

        private void OnDisable()
        {
            _gameInput.Disable();

            _gameInput.Player.Dodge.performed -= OnDodgeButtonPressed;
        }

        private void OnDodgeButtonPressed(InputAction.CallbackContext context)
        {
            _characterMovement.ExternalForces += _externalForceValue;
        }

        private void Update()
        {
            

            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                _characterMovement.ExternalForces += _externalForceValue;
            }

            var input = _gameInput.Player.Movement.ReadValue<Vector2>();
            _characterMovement.MovementInput = input;
        }

    }
}