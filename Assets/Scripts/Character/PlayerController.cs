using Assets.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : ColorChangeBase
    {
        public static PlayerController instance;

        [Header("")]
        public int powerCount;
        public int powerLimit = 10;

        [Header("")]
        [SerializeField]
        private float scaleAmount = 0.15f;
        [SerializeField]
        private float scaleTime = 0.25f;

        // Private vars
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _playerMovement = PlayerMovement.instance;
        }

        public void Upscale()
        {
            if (powerCount < powerLimit)
            {
                powerCount++;

                transform.DOScale(new Vector3(transform.localScale.x + scaleAmount,
                transform.localScale.y + scaleAmount,
                transform.localScale.z + scaleAmount), scaleTime);
            }

            _playerMovement.movementSpeed += _playerMovement.speedUp;
        }

        public void Descale()
        {
            powerCount--;

            if (powerCount > -1)
            {
                transform.DOScale(new Vector3(transform.localScale.x - scaleAmount,
                transform.localScale.y - scaleAmount,
                transform.localScale.z - scaleAmount), scaleTime);
            }

            _playerMovement.movementSpeed -= _playerMovement.speedUp;

            if (powerCount < 0)
            {
                Death();
            }
        }

        private void Death()
        {
            _playerMovement.movementSpeed = 0;
            _playerMovement.UpdatePlayerState(PlayerState.Dead);

            GameManager.instance.UpdateGameState(GameState.Lose);
        }
    }
}