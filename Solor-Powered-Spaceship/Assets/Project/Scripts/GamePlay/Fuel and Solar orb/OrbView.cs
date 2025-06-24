using System;
using UnityEngine;

namespace Solar.Orb
{
    public class OrbView : MonoBehaviour
    {
        private OrbController orbController;
        public void SetController(OrbController _orbController) => orbController = _orbController;

        public void Update()
        {
            if (orbController == null) return;
            orbController.BehindThePlayer();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                orbController.OnTriggerWithPayer();
                Debug.Log("Collider iwth player");
            }
        }
    }
}