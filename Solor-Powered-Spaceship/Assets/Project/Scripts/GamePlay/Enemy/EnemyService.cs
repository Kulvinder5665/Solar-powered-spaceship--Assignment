using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Solar.Enemy
{

    public class EnemyService
    {
      
        #region Dependencies
  
         private EnemyScriptableObject enemyScriptableObject;
        private EnemyPool enemyPool;
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
        public EnemyService(EnemyView enemyPrefab, EnemyScriptableObject enemyScriptableObject, Transform playerPos)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            enemyPool = new EnemyPool(enemyPrefab, enemyScriptableObject.enemydata);
            playerTransform = playerPos;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            isSpawning = true;
            currentSpawnRate = enemyScriptableObject.initialSpawnRate;
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
               //IncreaseDifficulty();
                ResetSpawnTimer();
            }

        }

        #region Spawinig Enemies

        private void SpawnEnemy()
        {
            EnemyController spawnedEnemy = enemyPool.GetEnemy();
            spawnedEnemy.enemyView.gameObject.SetActive(true);
            spawnDistanceAhed = Random.Range(80, 200);
            Vector3 spawnPos = new Vector3(playerTransform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
                                           playerTransform.position.y + Random.Range(-spawnRangeY, spawnRangeY)
                                            , playerTransform.position.z + spawnDistanceAhed);

            spawnedEnemy.enemyView.transform.position = spawnPos;
            
         

            if (spawnedEnemy.enemyData.enemyType == EnemyType.Destructible)
            {
                spawnedEnemy.enemyView.GetComponent<Renderer>().material.color = Color.red;

            }
            else
            {
                spawnedEnemy.enemyView.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.black, 0.5f);

            }
        }
        #endregion
        private void ResetSpawnTimer() => spawnTimer = currentSpawnRate;
        public void SetEnemySpawning(bool setActive) => isSpawning = setActive;
        public void ReturnEnemyToPool(EnemyController enemyToReturn) => enemyPool.ReturnItem(enemyToReturn);
    
     
    }
}