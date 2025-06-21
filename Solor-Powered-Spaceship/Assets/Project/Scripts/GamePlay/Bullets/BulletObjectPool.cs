using Solar.Bullet;
using UnityEngine;
using UnityEngine.TextCore;
using Solar.Utilities;
using UnityEditorInternal.Profiling.Memory.Experimental;
namespace Solar.Bullet {
    public class BulletObjectPool : GenericObjectPool<BulletController>
    {

        private BulletsScriptableObject bulletData;
        private BulletsView bulletsViewPrefab;
        public BulletObjectPool(BulletsView _bulletsViewPrefab, BulletsScriptableObject _bulletData)
        {
            this.bulletData = _bulletData;
            bulletsViewPrefab = _bulletsViewPrefab;
        }

        public BulletController GetBullet() => GetItem<BulletController>();

        protected override BulletController CreateItem<U>()
        {
            if (bulletsViewPrefab == null)
            {
                Debug.LogError("Bullet view Prefab is null in BulletObjectPool");
            }
          return new BulletController(bulletsViewPrefab, bulletData);
        }
        }
}