using Assets.Scripts.Managers;
using System;
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
                default:
                    break;
            }

            OnPlayerStateChanged?.Invoke(newState);
        }

        private void MoveForward(GameState state)
        {
            if (state == GameState.Started)
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

        private void JoystickSideMovementHandle()
        {
            if (state == PlayerState.Moving)
            {
                float xMovement = UltimateTouchpad.GetHorizontalAxis("JoyStick");

                transform.position += new Vector3(xMovement, 0, 0) * sideMovementSpeed * Time.deltaTime;
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
        Dead
    }
}