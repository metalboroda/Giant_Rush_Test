using Assets.Scripts.Character;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimation : MonoBehaviour
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

    // Private comp
    private Animator animator;

    // UniRx refs
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerStateChanged += IdleAnimation;
        PlayerMovement.OnPlayerStateChanged += RunAnimation;
        PlayerMovement.OnPlayerStateChanged += FightIdleAnimation;
        PlayerMovement.OnPlayerStateChanged += PunchAnimation;
        PlayerMovement.OnPlayerStateChanged += ReceivePunchAnimation;
        PlayerMovement.OnPlayerStateChanged += Death;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerStateChanged -= IdleAnimation;
        PlayerMovement.OnPlayerStateChanged -= RunAnimation;
        PlayerMovement.OnPlayerStateChanged -= FightIdleAnimation;
        PlayerMovement.OnPlayerStateChanged -= PunchAnimation;
        PlayerMovement.OnPlayerStateChanged -= ReceivePunchAnimation;
        PlayerMovement.OnPlayerStateChanged -= Death;
    }

    private void IdleAnimation(PlayerState state)
    {
        if (state == PlayerState.Idle)
        {
            animator.CrossFade(animationNames.IdleHash, transitionDuration);
        }
    }

    private void RunAnimation(PlayerState state)
    {
        if (state == PlayerState.Moving)
        {
            animator.CrossFade(animationNames.RunHash, transitionDuration);
        }
    }

    private void FightIdleAnimation(PlayerState state)
    {
        if (state == PlayerState.FighIdle)
        {
            animator.CrossFade(animationNames.FightIdleHash, transitionDuration);
        }
    }

    private void PunchAnimation(PlayerState state)
    {
        if (state == PlayerState.Punching)
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

    private void ReceivePunchAnimation(PlayerState state)
    {

    }

    private void Death(PlayerState state)
    {
        if (state == PlayerState.Dead)
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