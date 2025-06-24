using UnityEngine;
namespace Solar.Bullet
{
    public class BulletService
    {
        public BulletObjectPool bulletPool;

        public BulletService(BulletsView _bulletsView, BulletsScriptableObject _bulletsScriptableObject)
        {
            bulletPool = new BulletObjectPool(_bulletsView, _bulletsScriptableObject);
        }

        public BulletController GetBullet() => bulletPool.GetBullet();
        public void ReturnBulletToPool(BulletController bullet)
        {
            bulletPool.ReturnItem(bullet);
        }
       
    }

}