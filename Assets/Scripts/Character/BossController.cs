using Assets.Scripts.Managers;
using System;
using UnityEngine;

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

        [Header("Attack Params")]
        [SerializeField]
        private float punchInterval = 1f;

        // Private vars
        private float _punchTimer;

        // Private refs
        private FightController _fightController;

        private void Awake()
        {
            instance = this;

            _fightController = GetComponent<FightController>();
        }

        private void Start()
        {
            UpdateBossState(BossState.Idle);
        }

        private void Update()
        {
            Attack();
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

        private void Attack()
        {
            if (_fightController == null) return;

            _punchTimer += Time.deltaTime * 1;

            if (_punchTimer >= punchInterval)
            {
                _fightController.PunchBoss();

                _punchTimer = 0f;
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
        FightIdle,
        Punching,
        Dead
    }
}