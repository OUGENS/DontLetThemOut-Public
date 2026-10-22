using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    public static RoundUI I { get; private set; }

    [Header("Round Transition Splash")]
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private RoundSplashUI _splashUI;

    [Header("Wave Info HUD")]
    [SerializeField] private TextMeshProUGUI _txtWaveNumber;
    [SerializeField] private TextMeshProUGUI _txtTotalEnemiesCount;

    [Header("Enemy Preview List Container")]
    [SerializeField] private Transform _enemyCardsContainer;
    [SerializeField] private EnemyPreviewCard _enemyCardPrefab;

    [Header("Controls")]
    [SerializeField] private Button _btnWaveControl;
    [SerializeField] private TextMeshProUGUI _txtWaveControlLabel;

    private float _splashTimeRemaining;
    private bool _isSplashActive;
    private int _lastBanneredWave = -1;

    private readonly float[] _speedMultipliers = { 1.0f, 2.0f, 4.0f };
    private int _currentSpeedIndex;

    public float CurrentTargetSpeed => _speedMultipliers[_currentSpeedIndex];
    private int CurrentWaveIndex => RoundManager.I != null ? RoundManager.I.TotalRounds + 1 : 1;
    private bool IsPaused => GameControler.I != null && GameControler.I.GetCurrentState() is PauseState;

    private void Awake()
    {
        I = this;

        if (_btnWaveControl != null) _btnWaveControl.onClick.AddListener(OnClickWaveControl);
        if (_splashUI != null) _splashUI.HideBanner();
    }

    private void OnEnable()
    {
        if (RoundManager.I == null) return;

        RoundManager.I.OnRoundDataUpdated += UpdateRoundUI;
        RoundManager.I.OnWaveStarted += TriggerSplashBanner;

        if (RoundManager.I.CurrentRoundData != null)
        {
            UpdateRoundUI(CurrentWaveIndex, RoundManager.I.CurrentRoundData);
        }

        if (RoundManager.I.IsWaveInProgress && _lastBanneredWave != CurrentWaveIndex)
        {
            TriggerSplashBanner(CurrentWaveIndex);
        }
    }

    private void OnDisable()
    {
        if (RoundManager.I != null)
        {
            RoundManager.I.OnRoundDataUpdated -= UpdateRoundUI;
            RoundManager.I.OnWaveStarted -= TriggerSplashBanner;
        }

        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (!_isSplashActive) return;

        if (GameControler.I != null && GameControler.I.GetCurrentState() is PlayingState)
        {
            _splashTimeRemaining -= Time.unscaledDeltaTime;

            if (_splashTimeRemaining <= 0f)
            {
                _isSplashActive = false;
                if (_splashUI != null) _splashUI.HideBanner();
            }
        }
    }

    private void UpdateRoundUI(int waveNumber, RoundData roundData)
    {
        if (_txtWaveNumber != null) _txtWaveNumber.text = $"WAVE {waveNumber}";
        if (roundData == null) return;

        if (_txtTotalEnemiesCount != null)
        {
            _txtTotalEnemiesCount.text = $"Total Enemies: {roundData.GetTotalCreatureCount()}";
        }

        PopulateEnemyCards(roundData);
        RefreshControlButtonState();
    }

    private void PopulateEnemyCards(RoundData roundData)
    {
        if (_enemyCardsContainer == null) return;

        foreach (Transform child in _enemyCardsContainer) Destroy(child.gameObject);

        if (_enemyCardPrefab == null || roundData.Creatures == null) return;

        foreach (var creature in roundData.Creatures)
        {
            if (creature.Type == null) continue;
            EnemyPreviewCard card = Instantiate(_enemyCardPrefab, _enemyCardsContainer);
            card.Setup(creature.Type.PreviewIcon, creature.SpawnCount);
        }
    }

    public void RefreshControlButtonState()
    {
        bool isRunning = RoundManager.I != null && RoundManager.I.IsWaveInProgress;

        if (!isRunning)
        {
            _currentSpeedIndex = 0;
            SetControlLabel("");
        }
        else
        {
            SetControlLabel($"{CurrentTargetSpeed:0}x");
        }

        ApplyCurrentTimeScale();

        if (_btnWaveControl != null) _btnWaveControl.interactable = true;
    }

    private void TriggerSplashBanner(int waveNumber)
    {
        _lastBanneredWave = waveNumber;
        _splashTimeRemaining = _transitionTime;
        _isSplashActive = true;

        if (_splashUI != null)
        {
            _splashUI.ShowBanner($"WAVE {waveNumber}", waveNumber == 1);
        }

        RefreshControlButtonState();
    }

    private void OnClickWaveControl()
    {
        if (RoundManager.I == null || IsPaused) return;

        if (!RoundManager.I.IsWaveInProgress)
        {
            RoundManager.I.NextRound();
        }
        else
        {
            _currentSpeedIndex = (_currentSpeedIndex + 1) % _speedMultipliers.Length;
            SetControlLabel($"{CurrentTargetSpeed:0}x");
            ApplyCurrentTimeScale();
        }
    }

    public void ResetSpeed()
    {
        _currentSpeedIndex = 0;
        SetControlLabel("");
        ApplyCurrentTimeScale();
    }

    private void ApplyCurrentTimeScale()
    {
        if (!IsPaused)
        {
            Time.timeScale = CurrentTargetSpeed;
        }
    }

    private void SetControlLabel(string text)
    {
        if (_txtWaveControlLabel != null)
        {
            _txtWaveControlLabel.text = text;
        }
    }
}