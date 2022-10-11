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
    private float minRagdollEnableTime = 0.25f;
    [SerializeField]
    private float maxRagdollEnableTime = 1.25f;
    [SerializeField]
    private List<GameObject> parts = new List<GameObject>();

    // Private comp
    private Animator animator;

    // Private refs
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerStateChanged += IdleAnimation;
        PlayerMovement.OnPlayerStateChanged += RunAnimation;
        PlayerMovement.OnPlayerStateChanged += DeathRagdoll;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerStateChanged -= IdleAnimation;
        PlayerMovement.OnPlayerStateChanged -= RunAnimation;
        PlayerMovement.OnPlayerStateChanged -= DeathRagdoll;
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

    private void DeathRagdoll(PlayerState state)
    {
        if (state == PlayerState.Dead)
        {
            animator.CrossFade(animationNames.DeathHash, transitionDuration);

            RagdollEnable();
        }
    }

    private void RagdollEnable()
    {
        var randTime = Random.Range(minRagdollEnableTime, maxRagdollEnableTime);

        Observable.Timer(TimeSpan.FromSeconds(randTime)).Subscribe(_ =>
        {
            foreach (var item in parts)
            {
                var collider = item.GetComponent<Collider>();
                var rigidbody = item.GetComponent<Rigidbody>();

                animator.enabled = false;

                collider.enabled = true;
                rigidbody.isKinematic = false;

                _disposable.Clear();
            }
        }).AddTo(_disposable);
    }
}