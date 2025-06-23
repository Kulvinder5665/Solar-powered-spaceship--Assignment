using UnityEngine;

namespace Solar.Enemy{
    public class EnemyView : MonoBehaviour, IDamageable
    {

        private EnemyController enemyController;


    
        public void SetController(EnemyController _enemyController) => this.enemyController = _enemyController;


        private void OnTriggerEnter(Collider collision)
        {
            enemyController?.OnEnemyCollided(collision.gameObject);
        }
        public void TakeDamage(int damageToTake)
        {
            enemyController.TakeDamge(damageToTake);
        }

        private void Update()
        {
            if (enemyController == null) return;
              enemyController.OutOfPlayerView();
        }
        
    }
}