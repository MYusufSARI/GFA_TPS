using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Update()
        {

            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                _characterMovement.ExternalForces = new Vector3(10, 0, 0);

                _characterMovement.ExternalForces += _externalForceValue;
            }

            _characterMovement.MovementInput = _movementInput;
        }

    }
}