using UnityEngine;
namespace Solar.Bullet
{
    public class BulletsView : MonoBehaviour
    {
        private BulletController bulletController;
        public void SetController(BulletController _bulletController) => this.bulletController = _bulletController;

        void Update()
        {
            bulletController?.UpdateBulletMotion();
            bulletController.OnTimeEnded();
        }

        private void OggerEnter(Collider collision)
        {
            bulletController?.OnBulletEnteredTrigger(collision.gameObject);
        }
    }
}