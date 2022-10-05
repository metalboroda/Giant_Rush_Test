using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameHandler : MonoBehaviour
    {
        public static event Action OnGameStart;
        public static event Action OnLose;

        [Header("UI Screens")]
        [SerializeField]
        private GameObject startScreen;
        [SerializeField]
        private GameObject gameScreen;
        [SerializeField]
        private GameObject loseScreen;

        [Header("Game Screen")]
        [SerializeField]
        private TextMeshProUGUI powerCounterText;

        // Private refs
        private CharacterHandler characterHandler;

        private void OnEnable()
        {
            CharacterHandler.OnDeath += Lose;
        }

        private void OnDisable()
        {
            CharacterHandler.OnDeath -= Lose;
        }

        private void Awake()
        {
            characterHandler = FindObjectOfType<CharacterHandler>();
        }

        private void Update()
        {
            SetPlayerLevelText();
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();

            startScreen.SetActive(false);
            gameScreen.SetActive(true);
        }

        private void Lose()
        {
            OnLose?.Invoke();

            gameScreen.SetActive(false);
            loseScreen.SetActive(true);
        }

        private void SetPlayerLevelText()
        {
            if (characterHandler == null) return;

            powerCounterText.SetText(characterHandler._playerLevel.ToString());
        }
    }
}