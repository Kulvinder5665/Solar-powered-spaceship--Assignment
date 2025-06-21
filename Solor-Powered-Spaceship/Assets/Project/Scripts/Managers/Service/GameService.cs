using Solor.Player;
using Solor.UI;
using UnityEngine;

public class GameService : MonoBehaviour
{
    #region Dependencies
    private PlayerService playerService;

    [SerializeField] private UIView uiService;
    #endregion

    #region Prefabs
    [SerializeField] private PlayerView playerPrefab;

    #endregion

    #region scriptable Objects
    [SerializeField] private PlayerConfig playerScriptableObject;
    #endregion

    private void Start()
    {
        playerService = new PlayerService(playerPrefab, playerScriptableObject);
    }

    private void Update()
    {

    }

    #region Getter
    public PlayerService GetPlayerService => playerService;
    public UIView GetUIService() => uiService;
    #endregion 
}