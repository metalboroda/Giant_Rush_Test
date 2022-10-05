using Assets.Scripts.Items;
using Assets.Scripts.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public float movementSpeed = 5;
        public bool canMove = false;

        [SerializeField]
        private List<GameObject> levelPrefabs = new List<GameObject>();
        [SerializeField]
        private List<GameObject> portalPrefabs = new List<GameObject>();
        [SerializeField]
        private int howMuchToPortal = 2;

        [SerializeField]
        private List<GameObject> spawnedLevels = new List<GameObject>();

        // Private vars
        private Vector3 spawnOffset;
        private int spawnedLevelsCount;
        private int portalIndex = 1;

        private void Start()
        {
            spawnedLevelsCount = howMuchToPortal;
        }

        private void Update()
        {
            spawnOffset = new Vector3(spawnedLevels[spawnedLevels.Count - 1].transform.position.x,
                spawnedLevels[spawnedLevels.Count - 1].transform.position.y,
                spawnedLevels[spawnedLevels.Count - 1].transform.position.z + 30);
        }

        private void OnEnable()
        {
            LevelController.OnMovementComplete += SpawnLevel;
            LevelController.OnMovementComplete += DestroyLevel;
            GameHandler.OnGameStart += StartMoveLevels;
        }

        private void OnDisable()
        {
            LevelController.OnMovementComplete -= SpawnLevel;
            LevelController.OnMovementComplete -= DestroyLevel;
            GameHandler.OnGameStart -= StartMoveLevels;
        }

        private void StartMoveLevels()
        {
            canMove = true;
        }

        public void SpawnLevel()
        {
            spawnedLevelsCount++;

            if (spawnedLevelsCount >= howMuchToPortal)
            {
                SpawnPortal();
            }
            else
            {
                var spawnedLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Count)], spawnOffset, Quaternion.identity);

                spawnedLevels.Add(spawnedLevel);
            }
        }

        private void SpawnPortal()
        {
            if (portalIndex == 1)
            {
                var spawnedPortal = Instantiate(portalPrefabs[Random.Range(0, portalPrefabs.Count)], spawnOffset, Quaternion.identity);
                var portal = spawnedPortal.GetComponent<Portal>();

                portal.SetPortal(1);

                portalIndex = 0;

                spawnedLevels.Add(spawnedPortal);
            }
            else
            {
                var spawnedPortal = Instantiate(portalPrefabs[Random.Range(0, portalPrefabs.Count)], spawnOffset, Quaternion.identity);
                var portal = spawnedPortal.GetComponent<Portal>();

                portal.SetPortal(0);

                portalIndex = 1;

                spawnedLevels.Add(spawnedPortal);
            }

            spawnedLevelsCount = 0;
        }

        private void DestroyLevel()
        {
            Destroy(spawnedLevels[0]);

            spawnedLevels.RemoveAt(0);
        }
    }
}