using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS;
using GFA.TPS.Input;
using GFA.TPS.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFA.TPS.Mediators
{

    public class PlayerMediator : MonoBehaviour
    {
        private CharacterMovement _characterMovement;

        private GameInput _gameinput;

        [SerializeField]
        private float _dodgePower;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();

            _gameinput = new GameInput();
        }

        private void OnEnable()
        {
            _gameinput.Enable();
            _gameinput.Player.Dodge.performed += OnDodgeRequested;
        }

        private void OnDisable()
        {
            _gameinput.Disable();
        }


        private void OnDodgeRequested(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _characterMovement.Velocity.normalized * _dodgePower;
        }


        private void Update()
        {

        }
    }

}
