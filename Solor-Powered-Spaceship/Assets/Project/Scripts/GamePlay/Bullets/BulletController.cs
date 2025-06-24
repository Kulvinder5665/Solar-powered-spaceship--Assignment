
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
        public void Deactivate()
        {
            if (bulletsView != null)
                bulletsView.gameObject.SetActive(false);
        }

        void InitizleVar()
        {
            currentTime = bulletsScriptableObject.lifeTime;
        }

        public void ConfigureBullet(Transform spawnTrans)
        {
            InitizleVar();

            bulletsView.gameObject.SetActive(true);
            bulletsView.transform.position = spawnTrans.position;


        }
        public void UpdateBulletMotion() => bulletsView.transform.Translate(Vector3.up * Time.deltaTime * bulletsScriptableObject.speed);

        public void OnBulletEnteredTrigger(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<IDamageable>() != null)
            {
                collidedObject.GetComponent<IDamageable>().TakeDamage(bulletsScriptableObject.damge);
                //GameService.Instance
                bulletsView.gameObject.SetActive(false);
                GameService.Instance.GetBulletService().ReturnBulletToPool(this);

            }
        }

        public void OnTimeEnded()
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                GameService.Instance.GetBulletService().ReturnBulletToPool(this);
            }
        }

        public BulletsView GetView() => bulletsView;
    }


}
