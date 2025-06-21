using Solar.Bullet;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
      


        public PlayerController(PlayerView _playerView,
                                 PlayerConfig _playerConfig, BulletObjectPool _bulletPool)
        {
            this.playerView = Object.Instantiate(_playerView);
            playerView.SetController(this);
            playerConfig = _playerConfig;
            playerView.Initialized();
            playerView.ConnectController();
            this.bulletPool = _bulletPool;
            InitializeVaribale();
        }

        private void InitializeVaribale()
        {
            this.rb = playerView.GetRigidbody();
           
           
        }


        #region Move Functions
        public void MoveForward()
        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerConfig.forwardspeed);
        }
        public void LeftRightMovement()
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                Vector2 touchpos = Touchscreen.current.primaryTouch.position.ReadValue();
                if (touchpos.x > Screen.width / 2)
                {
                    playerConfig.isThrusting = true;
                }
                else
                {
                    playerConfig.isThrusting = false;
                }

                Vector2 delta = Touchscreen.current.primaryTouch.delta.ReadValue();

                float dragAmountX = delta.x * playerConfig.TouchSensitivity * Time.deltaTime;
                Vector3 pos = playerView.transform.position;
                pos.x += dragAmountX;
                playerView.transform.position = pos;

            }
            else
            {
                playerConfig.isThrusting = false;
            }
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
                if (rb.linearVelocity.y < 0)
                {
                    Vector3 vel = rb.linearVelocity;
                    vel.y = 0f;
                    rb.linearVelocity = vel;

                }
                if (rb.position.y > 0)
                {
                    Vector3 pos = rb.position;
                    pos.y = Mathf.Max(0f, pos.y - playerConfig.fallSpeed * Time.fixedDeltaTime);
                    rb.MovePosition(pos);
                }
                else if (rb.position.y < 0)
                {
                    Vector3 pos = rb.position;
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