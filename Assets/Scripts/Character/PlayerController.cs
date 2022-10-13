using Assets.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : ColorChangeBase, IDamageable
    {
        public static PlayerController instance;

        [Header("")]
        public int powerCount = 1;
        public int powerLimit = 99;

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

            if (powerCount < 1)
            {
                Death();
            }
        }

        private void GetHit(int damageAmount)
        {
            powerCount -= damageAmount;

            if (powerCount <= 0)
            {
                Death();
            }
        }

        public void Damage(int damageAmount)
        {
            GetHit(damageAmount);
        }

        private void Death()
        {
            _playerMovement.movementSpeed = 0;
            _playerMovement.UpdatePlayerState(PlayerState.Dead);

            GameManager.instance.UpdateGameState(GameState.RunnerLose);
        }
    }
}