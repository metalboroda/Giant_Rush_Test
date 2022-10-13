using Assets.Scripts.Character;
using Assets.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class FightTrigger : MonoBehaviour
    {
        [SerializeField]
        private Transform fightPoint;
        private float movementSpeed;

        // Private refs
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = PlayerMovement.instance;

            movementSpeed = _playerMovement.movementSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.DOMove(new Vector3(fightPoint.position.x,
                    other.transform.position.y,
                    fightPoint.position.z), movementSpeed)
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
            _playerMovement.UpdatePlayerState(PlayerState.FighIdle);
        }
    }
}