using Assets.Scripts.Managers;
using System;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class BossController : MonoBehaviour
    {
        public static BossController instance;

        // Events
        public static event Action<BossState> OnBossStateChanged;

        [Header("")]
        [SerializeField]
        private BossState state;

        [Header("")]
        public int powerCount;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            UpdateBossState(BossState.Idle);
        }

        public void UpdateBossState(BossState newState)
        {
            state = newState;

            switch (newState)
            {
                case BossState.Idle:
                    break;
                case BossState.FightIdle:
                    break;
                case BossState.Punching:
                    break;
                case BossState.Dead:
                    break;
                default:
                    break;
            }

            OnBossStateChanged?.Invoke(newState);
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
        FightIdle,
        Punching,
        Dead
    }
}