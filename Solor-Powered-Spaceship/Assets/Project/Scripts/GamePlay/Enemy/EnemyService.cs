using System.Collections.Generic;

using UnityEngine;

namespace Solar.Enemy
{

    public class EnemyService
    {

        #region Dependencies

        private Dictionary<EnemyType, EnemyPool> enemyPools;
        private EnemyScriptableObject[] enemyScriptableObject;

        #endregion

        #region Variables
        private bool isSpawning;
        private float currentSpawnRate;
        private float spawnTimer;
        private Transform playerTransform;

        private float spawnRangeX = 200f;
        private float spawnRangeY = 30f;
        private float spawnDistanceAhed = 200f;
        #endregion

        #region Initialization
        public EnemyService(EnemyView enemyPrefab, EnemyScriptableObject[] enemyScriptableObject, Transform playerPos)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            playerTransform = playerPos;

            enemyPools = new Dictionary<EnemyType, EnemyPool>();

            foreach (var so in enemyScriptableObject)
            {
                var enemyType = so.enemydata.enemyType;
                if (!enemyPools.ContainsKey(enemyType))
                {
                    enemyPools.Add(enemyType, new EnemyPool(enemyPrefab, so.enemydata));
                }
                currentSpawnRate = so.initialSpawnRate;
            }
            //       enemyPool = new EnemyPool(enemyPrefab, enemyScriptableObject.enemydata);

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            isSpawning = true;

            spawnTimer = currentSpawnRate;

        }
        #endregion
        public void Update()
        {
            // logic for enemy spawan
            if (!isSpawning) return;
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                IncreaseDifficulty();
                ResetSpawnTimer();
            }

        }

        #region Spawinig Enemies

        private void SpawnEnemy()
        {
            var randomSo = enemyScriptableObject[Random.Range(0, enemyScriptableObject.Length)];
            var enemyType = randomSo.enemydata.enemyType;

            EnemyPool selectPool = enemyPools[enemyType];
            EnemyController enemyController = selectPool.GetEnemy();
            enemyController.enemyView.gameObject.SetActive(true);
            // EnemyController spawnedEnemy = enemyPool.GetEnemy();

            spawnDistanceAhed = Random.Range(80, 200);
            Vector3 spawnPos = new Vector3(playerTransform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
                                           playerTransform.position.y + Random.Range(-spawnRangeY, spawnRangeY),
                                           playerTransform.position.z + spawnDistanceAhed);

            enemyController.enemyView.transform.position = spawnPos;



            if (enemyController.enemyData.enemyType == EnemyType.Destructible)
            {
                enemyController.enemyView.GetComponent<Renderer>().material.color = Color.red;

            }
            else
            {
                enemyController.enemyView.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.black, 0.5f);

            }
        }

        private void IncreaseDifficulty()
        {
            //nothing for now

        }
        #endregion

        #region pool 
        private void ResetSpawnTimer() => spawnTimer = currentSpawnRate;
        public void SetEnemySpawning(bool setActive) => isSpawning = setActive;
        public void ReturnEnemyToPool(EnemyController enemyToReturn)
        {
            //enemyPool.ReturnItem(enemyToReturn);
            var enemyType = enemyToReturn.enemyData.enemyType;
            if (enemyPools.ContainsKey(enemyType))
            {
                enemyPools[enemyType].ReturnItem(enemyToReturn);
            }
            else
            {
                Debug.LogError($"No pool found Per enemy {enemyType}");
            }

        }
        #endregion
        #region Reset
        public void ResetEnemies()
        {

            foreach (var pool in enemyPools.Values)
            {
                foreach (var enemy in pool.pooledItems)
                {
                    enemy.Item.enemyView.gameObject.SetActive(false);
                }
            }
             SetEnemySpawning(true);
        }
        #endregion

    }
}