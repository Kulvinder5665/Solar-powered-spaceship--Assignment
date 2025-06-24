
using Solar.Enemy;
using UnityEngine;

namespace Solar.Orb
{
    public class OrbController 
    {
        public OrbView orbPrefab;
        public OrbData orbData;
        public OrbType currentOrbType;
        public OrbController(OrbView _orbPrefab, OrbData _orbData)
        {
            this.orbPrefab = Object.Instantiate(_orbPrefab);
            orbPrefab.SetController(this);

            this.orbData = _orbData;

        }

        public void OnTriggerWithPayer()
        {
            // if (collision.gameObject.GetComponent<Iinteractable>() != null)
            // {
                if (currentOrbType == OrbType.fuelOrb)
                {

                    GameService.Instance.GetPlayerService().GetPlayerController().RefillFuel(orbData.refillAmount);
                    ContReturnToPool();

                }
                else
                {
                  
                    GameService.Instance.GetPlayerService().GetPlayerController().RefillSolarOrb(orbData.refillAmount);
                     ContReturnToPool();

                }
           // }
        }

        // if orb z value is less then player return in the pool
        public  void BehindThePlayer()
        {
            if (orbPrefab.transform.position.z < GameService.Instance.playerTrans.position.z)
            {
                ContReturnToPool();
            }
        }
        // method to return to the Pool
        public void ContReturnToPool()
        {
            this.orbPrefab.gameObject.SetActive(false);
            GameService.Instance.GetOrbService().ReturnToPool(this);
        }
    }
}