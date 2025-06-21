using Solar.Player;
using Solar.UI;
using UnityEngine;
using Solar.Utilities;
using Solar.Bullet;
public class GameService : GenericMonoSingleton<GameService>
{
    #region Dependencies
    private PlayerService playerService;
    
    [SerializeField] private UIView uiService;
    #endregion

    #region Prefabs
    [SerializeField] private PlayerView playerPrefab;
    [SerializeField] private BulletsView bulletsView;
    #endregion

    #region scriptable Objects
    [SerializeField] private PlayerConfig playerScriptableObject;
    [SerializeField] private BulletsScriptableObject bulletData;

    #endregion

    private void Start()
    {
        playerService = new PlayerService(playerPrefab, playerScriptableObject, bulletsView, bulletData);
        
    }

    private void Update()
    {

    }

    #region Getter
    public PlayerService GetPlayerService => playerService;
    public UIView GetUIService() => uiService;
    #endregion 
}