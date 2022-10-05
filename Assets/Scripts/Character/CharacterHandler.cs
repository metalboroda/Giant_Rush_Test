using DG.Tweening;
using System;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    // Static because he's the only one
    public static event Action OnDeath;

    [SerializeField]
    private float scaleIncrease = 0.1f;
    public int _playerLevel;
    //[SerializeField]
    //private int _health;

    private int playerLevel
    {
        get { return _playerLevel; }
        set
        {
            _playerLevel = value;

            if (_playerLevel < 0)
            {
                OnDeath?.Invoke();
            }

        }
    }

    /*private int health
    {
        get { return _health; }
        set
        {
            _health = value;
        }
    }*/

    /*private void CalculateHealth()
    {
        _health = _playerLevel / 2;
    }*/

    public void IncreaseScale()
    {
        if (playerLevel < 10)
            _playerLevel++;

        transform.DOScale(new Vector3(transform.lossyScale.x + scaleIncrease,
            transform.lossyScale.y + scaleIncrease,
            transform.lossyScale.z + scaleIncrease), 0.25f);
    }

    public void DecreaseScale()
    {
        if (playerLevel >= -1)
            playerLevel--;

        transform.DOScale(new Vector3(transform.lossyScale.x - scaleIncrease,
            transform.lossyScale.y - scaleIncrease,
            transform.lossyScale.z - scaleIncrease), 0.25f);
    }
}