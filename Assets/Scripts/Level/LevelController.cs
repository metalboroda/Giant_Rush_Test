using Assets.Scripts.Items;
using Assets.Scripts.Managers;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public static event Action OnMovementComplete;

        [Header("Scaler")]
        [SerializeField]
        private bool needSpawnScalers = true;
        [SerializeField]
        private List<GameObject> scalerPrefabs = new List<GameObject>();
        [SerializeField]
        private Transform pointsParent;
        [SerializeField]
        private List<Transform> scalerPoints = new List<Transform>();
        [SerializeField]
        private int minSpawn = 3;
        [SerializeField]
        private int maxSpawn = 5;

        // Private vars
        private bool moveOnce = false;

        // Private refs
        private LevelManager levelManager;

        private void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();

            InitializePoints();
            SpawnScaler();
        }

        private void Update()
        {
            MoveLevel();
        }

        private void OnEnable()
        {
            GameHandler.OnLose += StopLevels;
        }

        private void OnDisable()
        {
            CharacterHandler.OnDeath -= StopLevels;
        }

        private void InitializePoints()
        {
            foreach (Transform points in pointsParent)
            {
                scalerPoints.Add(points);
            }
        }

        private void SpawnScaler()
        {
            if (!needSpawnScalers) return;

            var randCount = Random.Range(minSpawn, maxSpawn);

            for (int i = 0; i < scalerPoints.Count && i < randCount; i++)
            {
                var randPoints = Random.Range(0, scalerPoints.Count);

                if (scalerPoints[randPoints].childCount <= 0)
                {
                    Instantiate(scalerPrefabs[0], scalerPoints[randPoints].transform.position, Quaternion.identity, scalerPoints[randPoints].transform);
                }

            }
        }

        private void MoveLevel()
        {
            if (!levelManager.canMove) return;

            if (!moveOnce)
            {
                moveOnce = true;

                DOTween.defaultAutoKill = true;

                transform.DOMoveZ(-20, levelManager.movementSpeed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(LevelMovementEnd);
            }
        }

        private void StopLevels()
        {
            DOTween.KillAll();
        }

        private void LevelMovementEnd()
        {
            OnMovementComplete?.Invoke();
        }
    }
}