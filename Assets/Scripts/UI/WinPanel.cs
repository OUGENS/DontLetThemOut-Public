using UnityEngine;
using UnityEngine.UI;

public class WinPanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.Win;

    [Header("Win Buttons")]
    [SerializeField] private Button _btnChallenge;
    [SerializeField] private Button _btnWinRestart;
    [SerializeField] private Button _btnQuitGame;

    private void Start()
    {
        if (_btnWinRestart != null) _btnWinRestart.onClick.AddListener(OnClickRestart);
        if (_btnQuitGame != null) _btnQuitGame.onClick.AddListener(OnClickQuit);
        if (_btnChallenge != null) 
        {
            _btnChallenge.onClick.AddListener(OnCLickChallenge);
            _btnChallenge.onClick.AddListener(OnClickRestart);
        } 
    }

    private void OnCLickChallenge() 
    {
        if (RoundManager.I != null)
        {
            RoundManager.I.MaxRounds += 10;
        }
    }

    private void OnClickRestart()
    {
        if (GameControler.I != null)
        {
            GameControler.I.RestartGameWithoutReload();
        }

        UIManager.I.ShowPanelSingle(UIPanelType.Gameplay);
    }

    private void OnClickQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
