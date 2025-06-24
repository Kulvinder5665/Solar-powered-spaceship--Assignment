using Solar.Bullet;
using UnityEngine;
namespace Solar.Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        //private BulletObjectPool bulletPool;

        public PlayerService(PlayerView playerViewPrefab,
                             PlayerConfig playerScriptableObject)
        {
           // bulletPool = new BulletObjectPool(bulletPrefab, bulletsScriptableObject);
            playerController = new PlayerController(playerViewPrefab, playerScriptableObject);
        }

        public PlayerController GetPlayerController() => playerController;
        public Vector3 GetPlayerPosition() => playerController.GetPlayerPos();
        //public void ReturnBulletToPool(BulletController bulletToReturn) => bulletPool.ReturnItem(bulletToReturn);
    }
}