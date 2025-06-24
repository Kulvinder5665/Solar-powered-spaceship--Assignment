using System.Collections.Generic;
using Solar.Enemy;
using Solar.Utilities;
using UnityEngine;
namespace Solar.Orb{
    public class OrbService
    {
        #region  Dependencies
        public Dictionary<OrbType, OrbPool> orbPools;
        public OrbScriptableObect[] orbScriptableObects;
        public Transform playerTrans;
        #endregion

        #region varibale 
        public float spawnRangePosX = 10f;
        public float spawnRangePosY = 5f;
        public float spawnposZAhead = 100f;
        public bool isSpawning = false;
        private float currentSpawnRate;
        private float spawnTimer;

        #endregion

        #region Initialization
        public OrbService(OrbView _orbView, OrbScriptableObect[] orbScriptableObects, Transform _playerPos)
        {
            this.orbScriptableObects = orbScriptableObects;
            playerTrans = _playerPos;

            orbPools = new Dictionary<OrbType, OrbPool>();

            foreach (var so in orbScriptableObects)
            {
                var orbType = so.orbData.orbType;
                if (!orbPools.ContainsKey(orbType))
                {
                    orbPools.Add(orbType, new OrbPool(_orbView, so.orbData));
                }
                currentSpawnRate = so.initialSpawnRate;
            }
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            isSpawning = true;

            spawnTimer = currentSpawnRate;

        }

        #endregion

        #region  Spawn logic
        public void update()
        {
            if (isSpawning == false) return;
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnOrb();
                spawnTimer = currentSpawnRate;
            }
        }

        void SpawnOrb()
        {
            var randomSO = orbScriptableObects[Random.Range(0, orbPools.Count)];
            var enemyType = randomSO.orbData.orbType;

            OrbPool selectPool = orbPools[enemyType];
            OrbController orbController = selectPool.GetOrb();
            orbController.orbPrefab.gameObject.SetActive(true);

            spawnposZAhead = Random.Range(80, 200);
            Vector3 spawnPos = new Vector3(playerTrans.position.x + Random.Range(spawnRangePosX, spawnRangePosX),
                                           playerTrans.position.y + Random.Range(0, spawnRangePosY),
                                           playerTrans.position.z + spawnposZAhead);

            orbController.orbPrefab.transform.position = spawnPos;

            if (orbController.orbData.orbType == OrbType.fuelOrb)
            {
                orbController.orbPrefab.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                orbController.orbPrefab.GetComponent<Renderer>().material.color = new Color(255f / 255f, 255f / 255f, 0f / 255f);
            }
        }
        #endregion

        #region pool
        public void SetOrbSpawning(bool setActive) => isSpawning = setActive;
        public void ReturnToPool(OrbController orb)
        {
            var orbType = orb.orbData.orbType;
            if (orbPools.ContainsKey(orbType))
            {
                orbPools[orbType].ReturnItem(orb);
            }
            else
            {
                Debug.LogError($"No orb pool is found orb type : {orbType} ");
            }
        }
        #endregion

        #region Reset
        public void ResetOrb()
        {

            foreach (var pool in orbPools.Values)
            {
                foreach (var orb in pool.pooledItems)
                {
                    orb.Item.orbPrefab.gameObject.SetActive(false);
                }
            }
             SetOrbSpawning(true);
        }
        #endregion

        }
}