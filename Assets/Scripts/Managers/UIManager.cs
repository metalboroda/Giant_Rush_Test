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
        }
    }
}