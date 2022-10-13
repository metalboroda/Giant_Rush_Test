using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Character
{
    public class BossController : MonoBehaviour, IDamageable
    {
        public static BossController instance;

        // Events
        public static event Action<BossState> OnBossStateChanged;

        [Header("")]
        [SerializeField]
        private BossState state;

        [Header("")]
        public int powerCount;

        [Header("Fight")]
        [SerializeField]
        private List<Collider> punchColliders = new List<Collider>();

        // UniRx refs
        private CompositeDisposable _disposable = new CompositeDisposable();

        // Private vars
        private bool _isRecovery = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            UpdateBossState(BossState.Idle);
        }

        private void Update()
        {
            Fight();
        }

        public void UpdateBossState(BossState newState)
        {
            state = newState;

            switch (newState)
            {
                case BossState.Idle:
                    break;
                case BossState.Fighting:
                    break;
                case BossState.Dead:
                    break;
                default:
                    break;
            }

            OnBossStateChanged?.Invoke(newState);
        }

        private void Fight()
        {
            if (state == BossState.Fighting)
            {
                Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
                {
                    if (_isRecovery) return;

                    _isRecovery = true;

                    _disposable.Clear();

                }).AddTo(_disposable);
            }
        }

        public void ResetRecovery()
        {
            var randTime = Random.Range(0.5f, 1f);

            Observable.Timer(TimeSpan.FromSeconds(randTime)).Subscribe(_ =>
            {
                _isRecovery = false;

                UpdateBossState(BossState.Fighting);

                _disposable.Clear();

            }).AddTo(_disposable);
        }

        public void EnablePunchColliders()
        {
            foreach (var item in punchColliders)
            {
                item.enabled = true;
            }
        }

        public void DisablePunchColliders()
        {
            foreach (var item in punchColliders)
            {
                item.enabled = false;
            }
        }

        private void GetHit(int damageAmount)
        {
            powerCount -= damageAmount;

            if (powerCount <= 0)
            {
                Death();
            }
        }

        public void Damage(int damageAmount)
        {
            ScoreManager.instance.DecreasePowerScore();
            GetHit(damageAmount);
        }

        private void Death()
        {
            UpdateBossState(BossState.Dead);

            GameManager.instance.UpdateGameState(GameState.FightingWin);
        }
    }

    public enum BossState
    {
        Idle,
        Fighting,
        Dead
    }
}