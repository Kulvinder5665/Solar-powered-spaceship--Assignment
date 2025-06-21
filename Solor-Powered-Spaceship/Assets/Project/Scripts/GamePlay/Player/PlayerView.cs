

using UnityEngine;
using UnityEngine.InputSystem;

namespace Solar.Player
{
    public class PlayerView : MonoBehaviour
    {

        private PlayerController playerController;


        // Game Components Reference
        private PlayerInput playerInput;
        public InputAction touchDeltaAction;
        public InputAction touchThrustAction;
        private Rigidbody rb;

        public Transform cannonTransformPoint;

     

        public void Initialized()
        {
            rb = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInput>();
            touchDeltaAction = playerInput.actions["Move"];
            touchThrustAction = playerInput.actions["Thrust"];
            playerController.playerConfig.currentEnergy = playerController.playerConfig.maxEnergy;
             playerController.playerConfig.currentFuel = playerController.playerConfig.maxEnergy;
            StartShooting();
        }

        public void StartShooting()
        {
             InvokeRepeating(nameof(FireFromController),2f, Random.Range(0.3f,0.5f));
        }
        private void FireFromController()
        {
            if (playerController != null)
            {
                playerController.HandleShooting();
            }
        }
        public void ConnectController()
        {
            touchThrustAction.canceled += OnThrustEnded;
            //playerController.playerConfig.currentEnergy = playerController.playerConfig.maxEnergy;
        }

        public void DisconnectController()
        {
            touchThrustAction.canceled -= OnThrustEnded;
        }

        public void OnThrustEnded(InputAction.CallbackContext ctx)
        {
            if (playerController != null)
            {
                playerController.playerConfig.isThrusting = false;
            }
        }

        void Update()
        {
            playerController.LeftRightMovement();
            playerController.FuelUpdate();
        }
        void FixedUpdate()
        {
            playerController.MoveForward();
            playerController.OnThrusting();
          
        }



        public void SetController(PlayerController _playerController) => this.playerController = _playerController;
        public Rigidbody GetRigidbody()
        {
            return rb;
        }


    }
}

