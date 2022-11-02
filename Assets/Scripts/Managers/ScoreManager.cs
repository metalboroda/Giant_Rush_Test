using Assets.Scripts.Character;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        [Header("")]
        [SerializeField]
        private TextMeshProUGUI powerScoreCountText;

        // Private refs
        private PlayerController _playerController;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _playerController = PlayerController.instance;
        }

        public void InCreasePowerScore()
        {
            powerScoreCountText.SetText($"{_playerController.powerCount}");
        }

        public void DecreasePowerScore()
        {
            powerScoreCountText.SetText($"{_playerController.powerCount}");
        }
    }
}