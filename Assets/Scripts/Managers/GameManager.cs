using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        // Events
        public static event Action<GameState> OnGameStateChanged;

        [Header("")]
        public GameState State;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;

            UpdateGameState(GameState.Start);
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;

            switch (newState)
            {
                case GameState.Start:
                    break;
                case GameState.Started:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
                default:
                    break;
            }

            OnGameStateChanged?.Invoke(newState);
        }

        public void StartGame()
        {
            UpdateGameState(GameState.Started);
        }
    }

    public enum GameState
    {
        Start,
        Started,
        Win,
        Lose
    }
}