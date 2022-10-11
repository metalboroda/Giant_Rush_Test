using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField]
        private GameObject startScreen;
        [SerializeField]
        private GameObject gameScreen;
        [SerializeField]
        private GameObject winScreen;
        [SerializeField]
        private GameObject loseScreen;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChange;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChange;
        }

        private void GameManagerOnGameStateChange(GameState state)
        {
            startScreen.SetActive(state == GameState.Start);
            gameScreen.SetActive(state == GameState.Started);
            winScreen.SetActive(state == GameState.Win);
            loseScreen.SetActive(state == GameState.Lose);
        }
    }
}