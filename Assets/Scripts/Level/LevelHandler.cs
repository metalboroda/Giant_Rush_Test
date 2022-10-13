using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelHandler : MonoBehaviour
    {
        [Header("Portal Params")]
        public bool needPortal;
        public GameObject portalObj;
        [SerializeField]
        private List<GameObject> portalDisablesPoints = new List<GameObject>();

        [Header("Level Params")]
        public bool needNextLevelCollider;
        [SerializeField]
        private BoxCollider nextLevelCollider;

        [Header("Spawner Params")]
        public bool needSpawnItems;
        [SerializeField]
        private int minSpawnCount, maxSpawnCount;
        [SerializeField]
        private List<Transform> itemPoints = new List<Transform>();

        [Header("Spawned Items")]
        [SerializeField]
        private List<GameObject> pickablePowerPrefabs = new List<GameObject>();

        // Private refs
        private LevelSpawner levelSpawner;

        private void Awake()
        {
            levelSpawner = GetComponentInParent<LevelSpawner>();
        }

        private void Start()
        {
            PortalDisableSomePoints();
            SpawnPickablePowerAtRandomPoint();
            NextLevelColliderSwitch();
            PortalSwitch();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (levelSpawner != null)
                {
                    levelSpawner.SpawnLevel();
                }
            }
        }

        private void NextLevelColliderSwitch()
        {
            if (nextLevelCollider == null) return;

            nextLevelCollider.enabled = needNextLevelCollider;
        }

        private void PortalSwitch()
        {
            if (portalObj == null) return;

            portalObj.SetActive(needPortal);
        }

        public void EnablePortal()
        {
            portalObj.SetActive(true);
        }

        private void PortalDisableSomePoints()
        {
            if (needPortal)
            {
                foreach (var point in portalDisablesPoints)
                {
                    point.SetActive(false);
                }
            }
        }

        private void SpawnPickablePowerAtRandomPoint()
        {
            if (needSpawnItems)
            {
                var rand = Random.Range(minSpawnCount, maxSpawnCount);

                for (int i = 0; i < rand; i++)
                {
                    var randPoint = Random.Range(0, itemPoints.Count);

                    if (itemPoints[randPoint].childCount == 0)
                    {
                        Instantiate(pickablePowerPrefabs[Random.Range(0, pickablePowerPrefabs.Count)],
                            itemPoints[randPoint].transform.position,
                            Quaternion.identity, itemPoints[randPoint].transform);
                    }
                }
            }
        }
    }
}