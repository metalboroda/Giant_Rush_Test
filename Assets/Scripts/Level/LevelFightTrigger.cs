using Assets.Scripts.Character;
using Assets.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelFightTrigger : MonoBehaviour
    {
        [SerializeField]
        private float bossFightPointOffset = 1.5f;
        private float movementSpeed;

        // Private refs
        private BossController _bossController;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _bossController = GetComponentInChildren<BossController>();
        }

        private void Start()
        {
            _playerMovement = PlayerMovement.instance;

            movementSpeed = _playerMovement.movementSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_bossController == null) return;

            if (other.CompareTag("Player"))
            {
                other.transform.DOMove(new Vector3(_bossController.transform.position.x,
                    other.transform.position.y,
                    _bossController.transform.position.z - bossFightPointOffset), movementSpeed)
                    .SetSpeedBased()
                    .SetEase(Ease.Linear)
                    .OnComplete(PlayerFight)
                    .SetAutoKill();
            }
        }

        private void PlayerFight()
        {
            GameManager.instance.UpdateGameState(GameState.Fighting);
            _playerMovement.Fight();
            _playerMovement.UpdatePlayerState(PlayerState.FightIdle);

            StartCoroutine(FightController.Instance.CanFightSwitchRoutine());
        }
    }
}