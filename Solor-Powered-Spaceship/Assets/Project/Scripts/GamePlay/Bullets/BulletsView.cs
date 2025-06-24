using UnityEngine;
namespace Solar.Bullet
{
    public class BulletsView : MonoBehaviour
    {
        private BulletController bulletController;
        void OnEnable()
        {
            GamerEventManager.OnPlayerDie += PlayerHasBeDead;
        }
        void Oisable()
        {
            GamerEventManager.OnPlayerDie -= PlayerHasBeDead;
        }
        public void SetController(BulletController _bulletController) => this.bulletController = _bulletController;

        void Update()
        {
            bulletController?.UpdateBulletMotion();
            bulletController.OnTimeEnded();
        }

        private void OnTriggerEnter(Collider collision)
        {


            bulletController?.OnBulletEnteredTrigger(collision.gameObject);
        }

        private void PlayerHasBeDead()
        {
            gameObject.SetActive(false);
            if (bulletController != null)
            {
                GameService.Instance.GetBulletService().ReturnBulletToPool(bulletController);
            }
        }
    }
}