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

        // Private vars
        //private bool _isRecovery = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            UpdateBossState(BossState.Idle);

            EnablePunchColliders();
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

        public void EnablePunchColliders()
        {
            foreach (var item in punchColliders)
            {
                item.enabled = true;
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