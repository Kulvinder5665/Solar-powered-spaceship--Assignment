using System.Threading.Tasks;
using Solar.Bullet;
using UnityEngine;

namespace Solar.Player
{
    public class PlayerController
    {
        // Dependenics
        private PlayerView playerView;
        public  PlayerConfig playerConfig;
        private BulletObjectPool bulletPool;

        // Varibales
        private Rigidbody rb;
        private int currentHealth;



        public PlayerController(PlayerView _playerView,
                                 PlayerConfig _playerConfig, BulletObjectPool _bulletPool)
        {
            this.playerView = Object.Instantiate(_playerView);
            playerConfig = _playerConfig;
            this.bulletPool = _bulletPool;

            playerView.SetController(this);

            playerView.Initialized();
            InitializeVaribale();
            playerView.ConnectController();
            playerView.Activate();
        }

        private void InitializeVaribale()
        {
              this.rb = playerView.GetRigidbody();
           currentHealth = playerConfig.maxHealth;
        
        }


        #region Move Functions
        public void MoveForward()
        {
            if (rb == null) return;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerConfig.forwardspeed);
        }
        public void LeftRightMovement(Vector2 delta)
        {
            //     if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            //     {
            // Vector2 touchpos = Touchscreen.current.primaryTouch.position.ReadValue();
            //   Vector2 delta = Touchscreen.current.primaryTouch.delta.ReadValue();


            //  playerConfig.isThrusting = touchpos.x > Screen.width / 2;



            float dragAmountX = delta.x * playerConfig.TouchSensitivity * Time.deltaTime;
            // Vector3 pos = playerView.transform.position;
            // pos.x += dragAmountX;
            // playerView.transform.position = pos;
            Vector3 targetPos = rb.position;
            targetPos.x += dragAmountX;

            rb.MovePosition(targetPos);

           // }
            // else
            // {
            //     playerConfig.isThrusting = false;
            // }
        }

        public void SetThrusting(bool thrusting)
        {
            playerConfig.isThrusting = thrusting;
        }
        public void OnThrusting()
        {

            if (playerConfig.isThrusting && playerConfig.currentEnergy > 0)
            {
                rb.AddForce(Vector3.up * playerConfig.thrustForce, ForceMode.Acceleration);
                playerConfig.currentEnergy -= playerConfig.energyDrainRate * Time.fixedDeltaTime;
                GamerEventManager.ChangeInSolarEnergy(playerConfig.currentEnergy, playerConfig.maxEnergy);
            }
            else
            {


                Vector3 pos = rb.position;

                
                if (pos.y > 0)
                {
             
                    pos.y = Mathf.Max(0f, pos.y - playerConfig.fallSpeed * Time.fixedDeltaTime);
                    rb.MovePosition(pos);
                }
                else if (pos.y < 0f)
                {
                 ;
                    pos.y = 0f;
                    rb.MovePosition(pos);

                }
            }

        }

        public void FuelUpdate()
        {
            if (playerConfig.currentEnergy > 0)
            {
                playerConfig.currentFuel -= playerConfig.FuelDrainRate * Time.deltaTime;
                GamerEventManager.ChangeInFuelEnergy(playerConfig.currentFuel, playerConfig.maxEnergy);
            }
        }
        public Vector3 GetPlayerPos() => playerView != null ? playerView.transform.position : default;

        //Player Health Logic 
        public void TakeDamage(int damageToTake){
            currentHealth -= damageToTake;
            if(currentHealth<=0){
                PlayerDeath();
            }
        }

        private async void PlayerDeath()
        {
            Object.Destroy(playerView.gameObject);
            // Stop the enemy spawn and power up spawn

            //Wait for the player ship Destruction
            await Task.Delay(playerConfig.deathDelay * 1000);
            GameService.Instance.GetUIService().EnableGameOverUI();

        }
        private void OnDestroy()
        {
            playerView.DisconnectController();
        }
        #endregion

//Fire Logic
        public void HandleShooting()
        {
            FireWeapon();
        }

        public void FireWeapon()
        {
            FireBulletAtPos(playerView.cannonTransformPoint);
        }

       public void FireBulletAtPos(Transform fireLoc)
        {
            BulletController bulletToFire = bulletPool.GetBullet();
            bulletToFire.ConfigureBullet(fireLoc);
            
        }
    }

}