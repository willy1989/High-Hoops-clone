using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private BallZMovement ballZMovement;
    [SerializeField] private BallXMovement ballXMovement;
    [SerializeField] private BallYMovement ballYMovement;

    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private DragInput dragInput;
    private EndArea endArea;
    [SerializeField] private BallDeath ballDeath;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;
    [SerializeField] private BlocksPositionManager blocksPositionManager;
    [SerializeField] private CameraManager cameraManager;

    protected override void Awake()
    {
        base.Awake();

        dragInput.FirstInputRegisteredEvent += GameStartPhase;
        ballColorManager.LoseEvent += GameLosePhase;
    }

    private void Start()
    {
        ballZMovement.CanMove = false;
        ballXMovement.CanMove = false;
        ballYMovement.CanMove = false;

        ResetGamePhase();
    }

    public void ResetGamePhase()
    {
        ballZMovement.CanMove = false;
        ballXMovement.CanMove = false;
        ballYMovement.CanMove = false;

        UIManager.Instance.Reset();

        levelLoader.LoadLevel();

        ballXMovement.Reset();
        ballYMovement.Reset();
        ballZMovement.Reset();
        ballColorManager.Reset();
        ballDeath.Reset();
        
        ballNavigationWaypointManager.SetNextTarget(levelLoader.CurrentLevel.FirstWayPoint, levelLoader.CurrentLevel.SecondWayPoint);

        endArea = levelLoader.CurrentLevel.EndArea;
        endArea.ReachLevelEndEvent += GameWinPhase;

        blocksPositionManager.SetNextBlockPositionSetterGroups(levelLoader.CurrentLevel.BlockPositionSetterGroups);

        blocksPositionManager.Reset();

        cameraManager.Reset();

        AutoPilotManager.Instance.Reset();

        dragInput.ListenToFirstInput();
    }

    public void GameStartPhase()
    {
        ballZMovement.CanMove = true;
        ballXMovement.CanMove = true;
        ballYMovement.CanMove = true;

        cameraManager.SwitchToFollowCamera();

        UIManager.Instance.ToggleStartScreen(false);
    }

    public void GameWinPhase()
    {
        ballZMovement.CanMove = false;
        ballXMovement.CanMove = false;
        ballYMovement.CanMove = false;

        levelLoader.IncrementLevelIndex();

        UIManager.Instance.ToggleWinScreen(true);
    }

    public void GameLosePhase()
    {
        ballZMovement.CanMove = false;
        ballXMovement.CanMove = false;
        ballYMovement.CanMove = false;

        UIManager.Instance.ToggleLoseScreen(true);
    }
}
