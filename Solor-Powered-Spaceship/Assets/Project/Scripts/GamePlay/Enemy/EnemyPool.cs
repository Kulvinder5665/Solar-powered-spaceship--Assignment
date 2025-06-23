using Solar.Utilities;
using UnityEngine;

namespace Solar.Enemy
{
    public class EnemyPool : GenericObjectPool<EnemyController>
    {
          private EnemyView enemyPrefab;
        private Enemydata enemyData;

        public EnemyPool(EnemyView enemyPrefab, Enemydata enemyData)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
        }

        public EnemyController GetEnemy() => GetItem<EnemyController>();

        protected override EnemyController CreateItem<T>() => new EnemyController(enemyPrefab, enemyData);
    }
}