using UnityEngine;
namespace Solor.Player
{
    public class PlayerService
    {
        private PlayerController playerController;

        public PlayerService(PlayerView playerViewPrefab, PlayerConfig playerScriptableObject)
        {
            playerController = new PlayerController(playerViewPrefab, playerScriptableObject);
        }

        public PlayerController GetPlayerController => playerController;
        public Vector3 GetPlayerPosition() => playerController.GetPlayerPos();

    }
}