using System; 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private BallZMovement ballZMovement;
    [SerializeField] private BallXMovement ballXMovement;
    [SerializeField] private BallYMovement ballYMovement;

    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private DragInput dragInput;
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallSpawnManager ballSpawnManager;
    [SerializeField] private BlocksPositionManager blocksPositionManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private OrbitalCameraRig orbitalCameraRig;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;

    public Action ResetGameEvent;

    protected override void Awake()
    {
        base.Awake();

        ballColorManager.ballCollisionOppositeColorEvent += GameLosePhase;
        ballNavigationWaypointManager.ballReachedEndEvent += GameWinPhase;
        dragInput.FirstInputRegisteredEvent += GameStartPhase;

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(ResetGamePhase);
        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(ResetGamePhase);
    }

    private void Start()
    {
        ResetGamePhase();
    }

    private void ResetGamePhase()
    {
        StartCoroutine(ResetGamePhaseCoroutine());
    }

    /// <summary>
    /// When a level start, we load the level gameobject. Every other aspect of the game
    /// is reset and cleaned. This way we don't have to reload the entire scene. 
    /// </summary>
    private IEnumerator ResetGamePhaseCoroutine()
    {
        levelLoader.LoadLevel();

        ballNavigationWaypointManager.PrepareWaypoints();

        blocksPositionManager.SetBlockPositionSetterGroups(levelLoader.CurrentLevel.BlockPositionSetterGroups);

        ResetGameEvent?.Invoke();

        yield return ballSpawnManager.SpawnBallModel();

        dragInput.StartListenToFirstInput();
    }

    private void GameStartPhase()
    {
        ballZMovement.ToggleMovement(onOff: true);
        ballXMovement.ToggleMovement(onOff: true);
        ballYMovement.ToggleMovement(onOff: true);

        cameraManager.SwitchToFollowCamera();

        UIManager.Instance.ToggleStartScreen(false);
    }

    private void GameWinPhase()
    {
        ballZMovement.ToggleMovement(onOff: false);
        ballXMovement.ToggleMovement(onOff: false);
        ballYMovement.ToggleMovement(onOff: false);

        ballYMovement.StartJumpInPlace();

        levelLoader.IncrementLevelIndex();

        cameraManager.SwitchToRotateAroundCamera();

        orbitalCameraRig.ToggleRotation();

        UIManager.Instance.ToggleWinScreen(true);
        UIManager.Instance.ResetAllAutoLetters();
    }

    private void GameLosePhase()
    {
        ballZMovement.ToggleMovement(onOff: false);
        ballXMovement.ToggleMovement(onOff: false);
        ballYMovement.ToggleMovement(onOff: false);

        UIManager.Instance.ToggleLoseScreen(true);
    }
}
