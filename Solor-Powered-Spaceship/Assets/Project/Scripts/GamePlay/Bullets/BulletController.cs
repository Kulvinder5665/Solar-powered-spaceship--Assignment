using Solar.Bullet;
using UnityEngine;
namespace Solar.Bullet
{
    public class BulletController : IBullets
    {
        private BulletsView bulletsView;
        private BulletsScriptableObject bulletsScriptableObject;
        float currentTime;
        public BulletController(BulletsView bulletsViewPrefab, BulletsScriptableObject _bulletsScriptableObject)
        {
            this.bulletsView = Object.Instantiate(bulletsViewPrefab);
            bulletsView.SetController(this);
            this.bulletsScriptableObject = _bulletsScriptableObject;
            InitizleVar();
       
        }

        void InitizleVar()
        {
            currentTime = bulletsScriptableObject.lifeTime;
        }

        public void ConfigureBullet(Transform spawnTrans)
        {
            bulletsView.gameObject.SetActive(true);
            bulletsView.transform.position = spawnTrans.position;
            // bulletsView.transform.rotation = spawnTrans.rotation;
        }
        public void UpdateBulletMotion() => bulletsView.transform.Translate(Vector3.up * Time.deltaTime * bulletsScriptableObject.speed);

        public void OnBulletEnteredTrigger(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<IDamageable>() != null)
            {
                collidedObject.GetComponent<IDamageable>().TakeDamage(bulletsScriptableObject.damge);
                //GameService.Instance
                bulletsView.gameObject.SetActive(false);
                GameService.Instance.GetPlayerService.ReturnBulletToPool(this);

            }
        }

        public void OnTimeEnded()
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                GameService.Instance.GetPlayerService.ReturnBulletToPool(this);
            }
        }
    }


}
