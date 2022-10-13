using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject runnerCamera;
        [SerializeField]
        private GameObject fightingCamera;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += CameraChange;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= CameraChange;
        }

        private void CameraChange(GameState state)
        {
            runnerCamera.SetActive(state == GameState.Runner);
            fightingCamera.SetActive(state == GameState.Fighting);
        }
    }
}