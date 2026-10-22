using UnityEngine;

public class GameControler : MonoBehaviour
{
    public static GameControler I { get; private set; }

    private IGameState _currentState;

    public IGameState GetCurrentState() => _currentState;

    private void Awake() => I = this;

    private void Start()
    {
        ChangeState(new TitleState());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState is PlayingState) PauseGame();
            else if (_currentState is PauseState) ResumeGame();
        }

        _currentState?.Tick();
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void StartGame()
    {
        ChangeState(new PlayingState());

        if (RoundManager.I != null)
        {
            RoundManager.I.ResetRounds();
        }
    }

    public void PauseGame() => ChangeState(new PauseState());
    public void ResumeGame() => ChangeState(new PlayingState());
    public void WinGame() => ChangeState(new WinState());

    public void GameOver()
    {
        ClearWorldState();
        ChangeState(new GameOverState());
    }

    public void RestartGameWithoutReload()
    {
        Time.timeScale = 1.0f;

        ClearWorldState();

        if (PlayerHealthManager.I != null)
        {
            PlayerHealthManager.I.gameObject.SetActive(true);
            PlayerHealthManager.I.ResetHealth();
        }

        PlayerHealthUI playerUI = FindFirstObjectByType<PlayerHealthUI>();
        if (playerUI != null)
        {
            playerUI.RefreshUI();
        }

        if (RoundManager.I != null)
        {
            RoundManager.I.ResetRounds();
        }

        if (CoinManager.I != null)
        {
            CoinManager.I.ResetCoins(300);
        }

        if (AudioManager.I != null)
        {
            AudioManager.I.StopMusic();
        }

        if (RoundUI.I != null)
        {
            RoundUI.I.ResetSpeed();
        }

        if (MovingFloor.I != null)
        {
            MovingFloor.I.IsMoving = false;
            MovingFloor.I.ImmediatelyMoveToStartPosition();
        }

        ChangeState(new PlayingState());
    }

    public void ClearWorldState()
    {
        CreatureBehaviour[] activeCreatures = FindObjectsByType<CreatureBehaviour>(FindObjectsSortMode.None);
        foreach (var creature in activeCreatures) creature.Release(creature);

        TurretBulletBehaviour[] activeBullets = FindObjectsByType<TurretBulletBehaviour>(FindObjectsSortMode.None);
        foreach (var bullet in activeBullets) bullet.Release(bullet);

        HealthUI[] activeEnemyHealthUIs = FindObjectsByType<HealthUI>(FindObjectsSortMode.None);
        foreach (var ui in activeEnemyHealthUIs) ui.Release(ui);

        CoinPopup[] activeCoinPopups = FindObjectsByType<CoinPopup>(FindObjectsSortMode.None);
        foreach (var coin in activeCoinPopups) coin.Release(coin);

        TurretPlacementPoint[] placementPoints = FindObjectsByType<TurretPlacementPoint>(FindObjectsSortMode.None);
        foreach (var point in placementPoints) point.SellTurret();

        if (TurretNodeUIManager.I != null)
        {
            TurretNodeUIManager.I.CloseMenu();
        }
    }
}