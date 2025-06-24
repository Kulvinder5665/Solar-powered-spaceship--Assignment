using Solar.Player;
using Solar.UI;
using UnityEngine;
using Solar.Utilities;
using Solar.Bullet;
using Solar.Enemy;
using System.Collections.Generic;
using Solar.Orb;


public class GameService : GenericMonoSingleton<GameService>
{
    #region Dependencies
    private PlayerService playerService;
    private EnemyService enemyService;
    private OrbService orbService;
    [SerializeField] private UIView uiService;


    #endregion

    #region Prefabs
    [SerializeField] private PlayerView playerPrefab;
    [SerializeField] private BulletsView bulletsPrefab;
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private OrbView orbView;
   // [SerializeField] private FollowPlayer followPlayer;
    #endregion

    #region scriptable Objects
    [SerializeField] private PlayerConfig playerScriptableObject;
    [SerializeField] private BulletsScriptableObject bulletData;
    [SerializeField] private EnemyScriptableObject[] enemyScriptableObjects;
    [SerializeField] private OrbScriptableObect[] orbScriptableObects;


    #endregion

    // variable 
    public Transform playerTrans;

    #region  events
    void OnEnable()
    {
        GamerEventManager.OnGameStarted += SpawnPlayer;
    }
    void OnDisable()
    {
        GamerEventManager.OnGameStarted -= SpawnPlayer;
    }
    #endregion
    private void Start()
    {
      // playerService = new PlayerService(playerPrefab, playerScriptableObject, bulletsView, bulletData);

    }

    private void Update()
    {
        if (enemyService != null)
        {
            enemyService.Update();

        }
        if (orbService != null)
        {
            orbService.update();
        }
      
    }

    public void SpawnPlayer()
    {

        playerService = new PlayerService(playerPrefab, playerScriptableObject, bulletsPrefab, bulletData);
        playerTrans = playerService.GetPlayerController().playerView.transform;
        enemyService = new EnemyService(enemyPrefab, enemyScriptableObjects, playerService.GetPlayerController().playerView.transform);
        orbService = new OrbService(orbView, orbScriptableObects, playerService.GetPlayerController().playerView.transform);
    }

    #region Getter
    public PlayerService GetPlayerService() => playerService;
    public EnemyService GetEnemyService() => enemyService;
    public OrbService GetOrbService() => orbService;
    public UIView GetUIService() => uiService;
    #endregion 
}