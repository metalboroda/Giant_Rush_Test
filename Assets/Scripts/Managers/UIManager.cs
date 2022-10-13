using System;
using System.Collections;
using UniRx;
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
        private GameObject punchButton;
        [SerializeField]
        private GameObject winScreen;
        [SerializeField]
        private GameObject loseScreen;

        // UniRx refs
        private CompositeDisposable _disposable = new CompositeDisposable();
        private CompositeDisposable _disposable2 = new CompositeDisposable();

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChange;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChange;

            StopAllCoroutines();
        }

        private void GameManagerOnGameStateChange(GameState state)
        {
            startScreen.SetActive(state == GameState.Start);
            gameScreen.SetActive(state == GameState.Runner || state == GameState.Fighting);

            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                punchButton.SetActive(state == GameState.Fighting);

                _disposable.Clear();

            }).AddTo(_disposable);

            if (state == GameState.FightingWin || state == GameState.RunnerLose)
            {
                ScreenDelay(state);
            }
        }

        private void ScreenDelay(GameState state)
        {
            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                winScreen.SetActive(state == GameState.FightingWin);
                loseScreen.SetActive(state == GameState.RunnerLose);

                print("EndScreen");

                _disposable2.Clear();

            }).AddTo(_disposable2);
        }
    }
}