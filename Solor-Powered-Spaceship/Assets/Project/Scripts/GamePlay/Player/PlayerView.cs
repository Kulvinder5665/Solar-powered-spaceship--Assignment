

using System.Data.Common;
using TMPro;
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
        public InputAction touchPositionAction;
        private Rigidbody rb;

        public Transform cannonTransformPoint;
       

        public void Activate()=>  enabled = true;

        public void Initialized()
        {
            rb = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInput>();

            touchDeltaAction = playerInput.actions["Move"];
            touchThrustAction = playerInput.actions["Thrust"];
            touchPositionAction = playerInput.actions["TouchPosition"];



            playerController.playerConfig.currentEnergy = playerController.playerConfig.maxEnergy;
            playerController.playerConfig.currentFuel = playerController.playerConfig.maxFuel;
            StartShooting();
        }


        // public void ConnectController()
        // {
        //   //  touchThrustAction.started += ctx => OnThrustStarted();
        //    // touchThrustAction.canceled += ctx => OnThrustEnded();

        // }

        // public void DisconnectController()
        // {
        //     //touchThrustAction.canceled -= ctx => OnThrustEnded();
        //   //  touchThrustAction.started -= ctx => OnThrustStarted();
            
        // }
     
     
        
  
        void Update()
        {
            //playerController.LeftRightMovement();
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0) {


                bool isThrusting = false;
                Vector2 totalDelta = Vector2.zero;
                foreach (var touch in Touchscreen.current.touches)
                {
                    if (touch.press.isPressed)
                    {
                        Vector2 pos = touch.position.ReadValue();
                        if (pos.x > Screen.width / 2)
                        {
                            isThrusting = true;
                        }
                        Vector2 delta = touch.delta.ReadValue();
                        totalDelta += delta;
                    }
                 }
           
                playerController.LeftRightMovement(totalDelta);
                playerController.SetThrusting(isThrusting);
            }
            else
            {

                playerController.SetThrusting(false);
                
            }
            playerController.FuelUpdate();
        }
      
        void FixedUpdate()
        {
            if (playerController == null) return;
            playerController.MoveForward();
            playerController.OnThrusting();

        }

        public void StartShooting()
        {
            InvokeRepeating(nameof(FireFromController), 2f, Random.Range(0.3f, 0.5f));
        }
        private void FireFromController()
        {
            if (playerController != null)
            {
                playerController.HandleShooting();
            }
        }

        public void SetController(PlayerController _playerController) => this.playerController = _playerController;
        public Rigidbody GetRigidbody()
        {
            return rb;
        }

     
    }
}

