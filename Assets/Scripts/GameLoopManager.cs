using System; 
using System.Collections;
using System.Collections.Generic;
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
        levelLoader.LoadLevel();

        ballNavigationWaypointManager.SetNextTarget(levelLoader.CurrentLevel.FirstWayPoint, levelLoader.CurrentLevel.SecondWayPoint);

        blocksPositionManager.SetNextBlockPositionSetterGroups(levelLoader.CurrentLevel.BlockPositionSetterGroups);

        ResetGameEvent?.Invoke();

        dragInput.ListenToFirstInput();
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
