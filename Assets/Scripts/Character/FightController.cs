using Assets.Scripts.Character;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightController : MonoBehaviour
{
    [Header("Fighter")]
    public FighterType fighterType;

    [Header("")]
    [SerializeField]
    private List<Collider> punchColl = new List<Collider>();

    // Private vars
    private bool _isRecovery = false;

    // Private comp
    private Animator _animator;

    // Private refs
    private HashAnimationNames _hashAnimationNames = new HashAnimationNames();
    private PlayerMovement _playerMovement;
    private BossController _bossController;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        switch (fighterType)
        {
            case FighterType.Player:
                _playerMovement = GetComponent<PlayerMovement>();
                break;
            case FighterType.Boss:
                _bossController = GetComponent<BossController>();
                break;
            default:
                break;
        }
    }

    public void EnablePunchLimbs()
    {
        foreach (var i in punchColl)
        {
            i.enabled = true;
        }
    }

    public void DisablePunchLimbs()
    {
        foreach (var i in punchColl)
        {
            i.enabled = false;
        }
    }

    public void Punch()
    {
        if (_isRecovery) return;

        if (_playerMovement != null)
        {
            _isRecovery = true;

            _playerMovement.UpdatePlayerState(PlayerState.Punching);

            PunchAnim();
        }
        else if (_bossController != null)
        {
            _isRecovery = true;

            _bossController.UpdateBossState(BossState.Punching);

            PunchAnim();
        }
    }

    private void PunchAnim()
    {
        var randPunch = Random.Range(0, 4 + 1);

        switch (randPunch)
        {
            case 0:
                _animator.CrossFade(_hashAnimationNames.PunchHash, 0.15f);
                break;
            case 1:
                _animator.CrossFade(_hashAnimationNames.PunchHash2, 0.15f);
                break;
            case 2:
                _animator.CrossFade(_hashAnimationNames.PunchHash3, 0.15f);
                break;
            case 3:
                _animator.CrossFade(_hashAnimationNames.PunchHash4, 0.15f);
                break;
            default:
                break;
        }
    }

    public void ResetRecovery()
    {
        if (_playerMovement != null)
        {
            _isRecovery = false;

            _playerMovement.UpdatePlayerState(PlayerState.FightIdle);
        }
        else if (_bossController != null)
        {
            _isRecovery = false;

            _bossController.UpdateBossState(BossState.FightIdle);
        }
    }

    public enum FighterType
    {
        Player,
        Boss
    }
}