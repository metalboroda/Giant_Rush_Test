using Assets.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelSpawner : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField]
        private int howManyToPortal = 2;

        [Header("")]
        [SerializeField]
        private List<GameObject> levelPrefabs = new List<GameObject>();

        [Header("")]
        [SerializeField]
        private List<GameObject> spawnedLevels = new List<GameObject>();

        // Private vars
        private int _levelSpawned;
        private int _portalColorIndex;

        // Private refs
        private LevelHandler _levelHandler;
        private PortalColor _portalColor;

        private void Start()
        {
            _levelSpawned = howManyToPortal - 1;
            _portalColorIndex = 0;
        }

        public void SpawnLevel()
        {
            _levelSpawned++;

            if (_levelSpawned != howManyToPortal)
            {
                var level = Instantiate(levelPrefabs[0], new Vector3(0, 0, spawnedLevels[spawnedLevels.Count - 1].transform.position.z + 30), Quaternion.identity, transform);

                spawnedLevels.Add(level);

                Destroy(spawnedLevels[0].gameObject);

                spawnedLevels.RemoveAt(0);
            }
            else
            {
                if (_portalColorIndex == 0)
                {
                    _portalColorIndex = 1;

                    SpawnPortal(1);
                }
                else if (_portalColorIndex == 1)
                {
                    _portalColorIndex = 0;

                    SpawnPortal(0);
                }
            }
        }

        private void SpawnPortal(int colorIndex)
        {
            _levelSpawned = 0;

            var level = Instantiate(levelPrefabs[0], new Vector3(0, 0, spawnedLevels[spawnedLevels.Count - 1].transform.position.z + 30), Quaternion.identity, transform);

            _levelHandler = level.GetComponent<LevelHandler>();
            _portalColor = _levelHandler.portalObj.GetComponent<PortalColor>();

            _levelHandler.needPortal = true;

            _portalColor.needColorAtStart = false;
            _portalColor.SetColor(colorIndex);

            spawnedLevels.Add(level);

            Destroy(spawnedLevels[0].gameObject);

            spawnedLevels.RemoveAt(0);
        }
    }
}