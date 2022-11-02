using Assets.Scripts.Character;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public static FightController Instance;

    [Header("Fighter")]
    public FighterType fighterType;

    [Header("")]
    public int punchPower = 3;
    public bool canFight = false;

    [Header("")]
    [SerializeField]
    private List<Collider> punchColl = new List<Collider>();

    // Private vars
    private bool _isRecovery = false;

    // Private refs
    private PlayerMovement _playerMovement;
    private BossController _bossController;

    private void Awake()
    {
        Instance = this;

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
        if (_isRecovery && !canFight) return;

        if (_playerMovement != null)
        {
            _isRecovery = true;

            _playerMovement.UpdatePlayerState(PlayerState.Punching);
        }

    }

    public void PunchBoss()
    {
        if (_isRecovery) return;
        if (!canFight) return;

        if (_bossController != null)
        {
            _isRecovery = true;

            _bossController.UpdateBossState(BossState.Punching);
        }
    }

    public IEnumerator CanFightSwitchRoutine()
    {
        yield return new WaitForSeconds(2f);

        canFight = true;
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