using System;
using UnityEngine;

namespace Solar.Orb
{
    public class OrbView : MonoBehaviour
    {
        private OrbController orbController;
        public void SetController(OrbController _orbController) => orbController = _orbController;
        void OnEnable()
        {
            GamerEventManager.OnPlayerDie += OnPlayerDie;
        }
        void OnDisable()
        {
            GamerEventManager.OnPlayerDie -= OnPlayerDie;
        }
        public void Update()
        {
            if (orbController == null) return;
            orbController.BehindThePlayer();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!gameObject.activeInHierarchy) return;
                orbController.OnTriggerWithPayer();
            
            }
        }

        void OnPlayerDie()
        {
            gameObject.SetActive(false);
            GameService.Instance.GetOrbService().ReturnToPool(orbController);

        }
    }
}