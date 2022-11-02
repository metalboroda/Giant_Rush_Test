using Assets.Scripts.Character;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAnimation : MonoBehaviour
{
    protected HashAnimationNames animationNames = new HashAnimationNames();

    [Header("Animator Params")]
    [SerializeField]
    private float transitionDuration = 0.05f;

    [Header("Ragdoll Params")]
    [SerializeField]
    private bool enableRagdollByTimer = true;
    [SerializeField]
    private float minRagdollEnableTime = 0.25f;
    [SerializeField]
    private float maxRagdollEnableTime = 1.25f;
    [SerializeField]
    private List<GameObject> parts = new List<GameObject>();

    // UniRx refs
    private CompositeDisposable _disposable = new CompositeDisposable();

    // Private comp
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        BossController.OnBossStateChanged += IdleAnimation;
        BossController.OnBossStateChanged += FightIdleAnimation;
        BossController.OnBossStateChanged += PunchAnimation;
        BossController.OnBossStateChanged += Death;
    }

    private void OnDisable()
    {
        BossController.OnBossStateChanged -= IdleAnimation;
        BossController.OnBossStateChanged -= FightIdleAnimation;
        BossController.OnBossStateChanged -= PunchAnimation;
        BossController.OnBossStateChanged -= Death;
    }

    private void IdleAnimation(BossState state)
    {
        if (state == BossState.Idle)
        {
            animator.CrossFade(animationNames.IdleHash, transitionDuration);
        }
    }

    private void FightIdleAnimation(BossState state)
    {
        if (state == BossState.FightIdle)
        {
            animator.CrossFade(animationNames.FightIdleHash, transitionDuration);
        }
    }

    private void PunchAnimation(BossState state)
    {
        if (state == BossState.Punching)
        {
            var randAnim = Random.Range(0, 3 + 1);

            switch (randAnim)
            {
                case 0:
                    animator.CrossFade(animationNames.PunchHash, transitionDuration);
                    break;
                case 1:
                    animator.CrossFade(animationNames.PunchHash2, transitionDuration);
                    break;
                case 2:
                    animator.CrossFade(animationNames.PunchHash3, transitionDuration);
                    break;
                case 3:
                    animator.CrossFade(animationNames.PunchHash4, transitionDuration);
                    break;
                default:
                    break;
            }
        }
    }

    private void Death(BossState state)
    {
        if (state == BossState.Dead)
        {
            var randomAnim = Random.Range(0, 3 + 1);

            switch (randomAnim)
            {
                case 0:
                    animator.CrossFade(animationNames.DeathHash, transitionDuration);
                    break;
                case 1:
                    animator.CrossFade(animationNames.DeathHash2, transitionDuration);
                    break;
                case 2:
                    animator.CrossFade(animationNames.DeathHash3, transitionDuration);
                    break;
                case 3:
                    animator.CrossFade(animationNames.DeathHash4, transitionDuration);
                    break;
                default:
                    break;
            }

            if (enableRagdollByTimer)
            {
                RagdollEnable();
            }
        }
    }

    public void RagdollEnable()
    {
        var randTime = Random.Range(minRagdollEnableTime, maxRagdollEnableTime);

        Observable.Timer(TimeSpan.FromSeconds(randTime)).Subscribe(_ =>
        {
            foreach (var item in parts)
            {
                animator.enabled = false;

                var collider = item.GetComponent<Collider>();
                var rigidbody = item.GetComponent<Rigidbody>();

                collider.enabled = true;
                rigidbody.isKinematic = false;
            }
            _disposable.Clear();

        }).AddTo(_disposable);
    }
}