using Assets.Scripts.Managers;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement instance;

        // Events
        public static event Action<PlayerState> OnPlayerStateChanged;

        [Header("")]
        public PlayerState state;

        [Header("")]
        public float movementSpeed = 5;
        public float speedUp = 0.25f;
        [SerializeField]
        private float sideMovementSpeed = 8;
        [SerializeField]
        private float sideConstraints;

        [Header("Rotation")]
        [SerializeField]
        private Transform model;
        [SerializeField]
        private float rotationLimit;
        [SerializeField]
        private float rotationSpeed = 0.25f;

        [Header("Fight")]
        [SerializeField]
        private List<Collider> punchColliders = new List<Collider>();

        // UniRx
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            UpdatePlayerState(PlayerState.Idle);
        }

        private void Update()
        {
            JoystickSideMovementHandle();
            ConstraintHandle();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += MoveForward;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= MoveForward;

            _disposable.Clear();
        }

        public void UpdatePlayerState(PlayerState newState)
        {
            state = newState;

            switch (newState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Moving:
                    break;
                case PlayerState.Dead:
                    break;
                case PlayerState.FighIdle:
                    break;
                case PlayerState.Punching:
                    break;
                default:
                    break;
            }

            OnPlayerStateChanged?.Invoke(newState);
        }

        private void MoveForward(GameState state)
        {
            if (state == GameState.Runner)
            {
                UpdatePlayerState(PlayerState.Moving);

                Observable.EveryUpdate().Subscribe(_ =>
                {
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                }).AddTo(_disposable);
            }
            else
            {
                _disposable.Clear();
            }
        }

        public void Fight()
        {
            UpdatePlayerState(PlayerState.FighIdle);
        }

        public void Punch(bool enableColliders)
        {
            UpdatePlayerState(PlayerState.Punching);

            if (enableColliders)
            {
                foreach (var item in punchColliders)
                {
                    item.enabled = true;
                }
            }
            else
            {
                foreach (var item in punchColliders)
                {
                    item.enabled = false;
                }
            }
        }

        private void JoystickSideMovementHandle()
        {
            if (state == PlayerState.Moving)
            {
                var xAxis = UltimateJoystick.GetHorizontalAxis("JoyStick");

                transform.position += new Vector3(xAxis * 10, 0, 0) * sideMovementSpeed * Time.deltaTime;

                RotationHandle();
            }
        }

        private void RotationHandle()
        {
            var xAxis = UltimateJoystick.GetHorizontalAxis("JoyStick");

            if (xAxis > rotationLimit)
            {
                model.DORotate(new Vector3(transform.rotation.x, 20, transform.rotation.z), rotationSpeed, default);
            }
            else if (xAxis < -rotationLimit)
            {
                model.DORotate(new Vector3(transform.rotation.x, -20, transform.rotation.z), rotationSpeed, default);
            }
            else if (xAxis < rotationLimit || xAxis > -rotationLimit || xAxis == 0)
            {
                model.DORotate(new Vector3(transform.rotation.x, 0, transform.rotation.z), rotationSpeed, default);
            }
        }

        private void ConstraintHandle()
        {
            var playerPos = new Vector3(Mathf.Clamp(transform.position.x, -sideConstraints, sideConstraints), transform.position.y, transform.position.z);

            transform.position = playerPos;
        }
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        FighIdle,
        Punching,
        Dead
    }
}