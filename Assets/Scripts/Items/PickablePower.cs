using Assets.Scripts.Character;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class PickablePower : ColorChangeBase
    {
        public bool needRandomColor = false;

        // Private refs
        private PlayerController _playerController;
        private ScoreManager _scoreManager;

        private void Start()
        {
            _playerController = PlayerController.instance;
            _scoreManager = ScoreManager.instance;

            if (needRandomColor)
            {
                RandomColorAtStart();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetOnTriggerScale(other);
                SetPlayerPower();

                Destroy(gameObject);
            }
        }

        private void RandomColorAtStart()
        {
            var randColorIndex = Random.Range(0, 2);

            SetColor(randColorIndex);
        }

        private void SetOnTriggerScale(Collider other)
        {
            if (_playerController.colorState == colorState)
            {
                _playerController.Upscale();
            }
            else
            {
                _playerController.Descale();
            }
        }

        private void SetPlayerPower()
        {
            if (_playerController.colorState == colorState)
            {
                _scoreManager.InCreasePowerScore();
            }
            else
            {
                _scoreManager.DecreasePowerScore();
            }
        }
    }
}