
using System.Threading.Tasks;
using Solar.Bullet;
using Unity.VisualScripting;
using UnityEngine;

namespace Solar.Player
{
    public class PlayerController
    {
        //Dependenics
        public PlayerView playerView;
        public PlayerConfig playerConfig;
        //private BulletObjectPool bulletPool;

        // Varibales
        private Rigidbody rb;
        private int currentHealth;
        public PlayerState currentPlayerState;



        public PlayerController(PlayerView _playerView,
                                 PlayerConfig _playerConfig)
                                
        {

            this.playerView = Object.Instantiate(_playerView);
            playerConfig = _playerConfig;
          //  this.bulletPool = _bulletPool;

            playerView.SetController(this);

            playerView.Initialized();
            InitializeVaribale();
            //   playerView.ConnectController();
         //   playerView.Activate();

        }

        private void InitializeVaribale()
        {
            this.rb = playerView.GetRigidbody();
            currentHealth = playerConfig.maxHealth;
            currentPlayerState = PlayerState.fire;
        }


        #region Move Functions
        public void MoveForward()
        {
            if (rb == null) return;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerConfig.forwardspeed);
        }

        public void LeftRightMovement(Vector2 delta)
        {

            float dragAmountX = delta.x * playerConfig.TouchSensitivity * Time.deltaTime;
            Vector3 targetPos = rb.position;
            targetPos.x += dragAmountX;
            rb.MovePosition(targetPos);

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
            if (playerConfig.currentFuel > 0 )
            {
                playerConfig.currentFuel -= playerConfig.FuelDrainRate * Time.deltaTime;
                playerConfig.currentFuel = Mathf.Max(0, playerConfig.currentFuel);
                GamerEventManager.ChangeInFuelEnergy(playerConfig.currentFuel, playerConfig.maxFuel);
            }
        }
        public Vector3 GetPlayerPos() => playerView != null ? playerView.transform.position : default;

        //Player Health Logic 
        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
            {
                currentPlayerState =   PlayerState.StopFire;
                PlayerDeath();
            }
        }

        private async void PlayerDeath()
        {
            GamerEventManager.PlayerHasDie();
           
            playerView.gameObject.SetActive(false);
            GameService.Instance.GetEnemyService().SetEnemySpawning(false);
            GameService.Instance.GetOrbService().SetOrbSpawning(false);

            //Wait for the player ship Destruction
            await Task.Delay(playerConfig.deathDelay * 1000);
            GameService.Instance.GetUIService().EnableGameOverUI();

        }

        #endregion

        #region  Fire Logic
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
            BulletController bulletToFire = GameService.Instance.GetBulletService().GetBullet();
            bulletToFire.ConfigureBullet(fireLoc);

        }
        #endregion


        #region  Refill orb and fuel


        public void RefillFuel(float AmountToAdd)
        {
            if (playerConfig.currentFuel < playerConfig.maxFuel)
            {
                playerConfig.currentFuel += AmountToAdd;
                if (playerConfig.currentFuel > playerConfig.maxFuel)
                {
                    playerConfig.currentFuel = playerConfig.maxFuel;
                }
                //playerConfig.currentFuel = Mathf.Clamp(playerConfig.currentFuel, 0, playerConfig.maxFuel);
                GamerEventManager.ChangeInFuelEnergy(playerConfig.currentFuel, playerConfig.maxFuel);
               // Debug.Log("Increase The Fuel");
            }

        }

        public void RefillSolarOrb(float AmountToAdd)
        {
            if (playerConfig.currentEnergy <= playerConfig.maxEnergy)
            {
                playerConfig.currentEnergy += AmountToAdd;
                playerConfig.currentEnergy = Mathf.Min(playerConfig.currentEnergy, playerConfig.maxEnergy);
                GamerEventManager.ChangeInSolarEnergy(playerConfig.currentEnergy, playerConfig.maxEnergy);

            }
        }


        #endregion


        #region  ResetPlayer
        public void ResetPlayer()
        {
            playerView.transform.position = new Vector3(0, 0, 0);
            rb.linearVelocity = Vector3.zero;


            currentHealth = playerConfig.maxHealth;
            currentPlayerState = PlayerState.fire;
            playerConfig.currentEnergy = playerConfig.maxEnergy;
            playerConfig.currentEnergy = playerConfig.maxFuel;

            playerView.gameObject.SetActive(true);
            
        }
        #endregion
        public enum PlayerState
        {
            fire,
            StopFire
        }
    }
}
