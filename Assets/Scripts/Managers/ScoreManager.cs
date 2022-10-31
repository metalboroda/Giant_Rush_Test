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
        private PlayerColorController _playerController;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _playerController = PlayerColorController.instance;
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