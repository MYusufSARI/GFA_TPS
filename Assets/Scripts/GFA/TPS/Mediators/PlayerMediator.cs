using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Input;
using GFA.TPS.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFA.TPS.Mediators
{
    public class PlayerMediator : MonoBehaviour
    {
        private CharacterMovement _characterMovement;

        private GameInput _gameInput;

        [SerializeField] private float _dodgePower;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero);

        private Camera _camera;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();

            _gameInput = new GameInput();

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDodgeRequested;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Dodge.performed -= OnDodgeRequested;
        }

        private void OnDodgeRequested(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _characterMovement.Velocity.normalized * _dodgePower;
        }


        private void Update()
        {
            var movementInput = _gameInput.Player.Movement.ReadValue<Vector2>();

            _characterMovement.MovementInput = movementInput;

            var ray = _camera.ScreenPointToRay(_gameInput.Player.PointerPosition.ReadValue<Vector2>());

            if (_plane.Raycast(ray, out float enter))
            {
                var worldPosition = ray.GetPoint(enter);
            }

        }
    }
}


