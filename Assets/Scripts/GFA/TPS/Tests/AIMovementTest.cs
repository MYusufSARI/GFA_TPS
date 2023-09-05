using System.Collections;
using System.Collections.Generic;
using GFA.TPS.MatchSystem;
using UnityEngine;

namespace GFA.TPS.Movement.Tests
{

    public class AIMovementTest : MonoBehaviour
    {
        [SerializeField]
        private float _accceptanceRadius;

        private CharacterMovement _characterMovement;

        [SerializeField]
        private MatchInstance _matchInstance;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _matchInstance.Player.transform.position);

            if (distance>_accceptanceRadius)
            {
                var direction = (_matchInstance.Player.transform.position - transform.position). normalized;
                _characterMovement.MovementInput = new Vector2(direction.x, direction.z);
            }
        }
    }
}