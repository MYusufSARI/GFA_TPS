using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Input;
using GFA.TPS.Movement;
using GFA.TPS;
using UnityEngine;
using UnityEngine.InputSystem;
using GFA.TPS.UI;
using GFA.TPS.UI.Popups;

namespace GFA.TPS.Mediators
{
    public class PlayerMediator : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private Attributes _attributes;
        public Attributes Attributes => _attributes;


        private CharacterMovement _characterMovement;

        private GameInput _gameInput;

        private Shooter _shooter;

        private XPCollectibleAttractor _xPCollectibleAttractor;

        [SerializeField] private float _dodgePower;

        private int _level;

        public int Level => _level;

        private float _xp;

        public float MaxHP => (_level + 1) * 5;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero);

        private Camera _camera;

        [SerializeField]
        private float _health;

        public event Action<int> LevelledUp;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();

            _shooter = GetComponent<Shooter>();

            _xPCollectibleAttractor = GetComponent<XPCollectibleAttractor>();

            _gameInput = new GameInput();

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDodgeRequested;
            _xPCollectibleAttractor.XPCollected += OnAttractorXPCollected;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Dodge.performed -= OnDodgeRequested;
            _xPCollectibleAttractor.XPCollected -= OnAttractorXPCollected;

        }


        private void OnAttractorXPCollected(float xp)
        {
            AddXP(xp);
        }

        private void AddXP(float value)
        {
            _xp += value;
            if (_xp >= MaxHP)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _level++;
            _xp = 0;
            PopupChannel.RequestPopup<BoosterSelectionPopup>();
            LevelledUp?.Invoke(_level);
        }



        private void OnDodgeRequested(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _characterMovement.Velocity.normalized * _dodgePower;
        }


        private void Update()
        {
            HandleAttributes();

            HandleMovement();

            if (_gameInput.Player.Shoot.IsPressed())
            {
                _shooter.Shoot();
            }
        }

        private void HandleAttributes()
        {
            _characterMovement.MovementSpeed = Attributes.MovementSpeed;
            _shooter.AttackSpeedMultipler = Attributes.AttackSpeed;
            _shooter.BaseDamage = Attributes.Damage;


        }

        private void HandleMovement()
        {
            var movementInput = _gameInput.Player.Movement.ReadValue<Vector2>();

            _characterMovement.MovementInput = movementInput;

            var ray = _camera.ScreenPointToRay(_gameInput.Player.PointerPosition.ReadValue<Vector2>());

            var gamePadLookDir = _gameInput.Player.Look.ReadValue<Vector2>();


            if (gamePadLookDir.magnitude > 0.1f)
            {
                var angle = -MathF.Atan2(gamePadLookDir.y, gamePadLookDir.x) * Mathf.Rad2Deg + 90;

                _characterMovement.Rotation = angle;
            }

            else
            {
                if (_plane.Raycast(ray, out float enter))
                {
                    var worldPosition = ray.GetPoint(enter);

                    var dir = (worldPosition - transform.position).normalized;

                    //Quaternion.LookRotation(dir). eulerAngles.y;
                    var angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + 90;

                    _characterMovement.Rotation = angle;
                }
            }
        }

        public void ApplyDamage(float damage, GameObject causer = null)
        {
            _health -= damage * (1 - Attributes.Defence);
        }
    }
}


