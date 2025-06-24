using Solar.Player;
using Unity.Android.Types;
using UnityEngine;
namespace Solar.Enemy
{
    public class EnemyController
    {
        // Dependencies
        public EnemyView enemyView;
        public Enemydata enemyData;


        // Variables
        private EnemyType currentEnemyType;
        private int currentHealth;


        public EnemyController(EnemyView _enemyView, Enemydata _enemyData)
        {
            this.enemyView = Object.Instantiate(_enemyView);
            enemyView.SetController(this);
            this.enemyData = _enemyData;
            Configure();
        }

        public void Configure()
        {
            currentEnemyType = enemyData.enemyType;

            currentHealth = enemyData.maxHealth;
            enemyView.gameObject.SetActive(true);
        }

        public void TakeDamge(int damageToTake)
        {
            if (currentEnemyType == EnemyType.Indestructible)
            {

                return;
            }

            currentHealth -= damageToTake;
            if (currentHealth <= 0)
            {
                EnemyDestroyed();
            }


        }
        public void UpdateEnemyMotion() => enemyView.transform.Translate(Vector3.back * Time.deltaTime * enemyData.moveSpeed);
        public void OnEnemyCollided(GameObject collidedGameObject)
        {

            if (collidedGameObject.GetComponent<PlayerView>() != null)
            {

                GameService.Instance.GetPlayerService().GetPlayerController().TakeDamage(enemyData.DamgeOnCollide);
                EnemyDestroyed();
            }
        }

        public void OutOfPlayerView()
        {

            if (this.enemyView.transform.position.z < GameService.Instance.playerTrans.position.z)
            {
                TakeDamge(1);
            }
        }
        void EnemyDestroyed()
        {
            if (currentEnemyType == EnemyType.Destructible)
            {
                enemyView.gameObject.SetActive(false);
                GameService.Instance.GetEnemyService().ReturnEnemyToPool(this);
            }
        }

      

    }
}