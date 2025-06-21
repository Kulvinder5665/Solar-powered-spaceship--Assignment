using UnityEngine;
namespace Solar.Bullet
{
    public interface IBullets
    {
        public void UpdateBulletMotion();
        public void OnBulletEnteredTrigger(GameObject collidedObject);
        
    }
}