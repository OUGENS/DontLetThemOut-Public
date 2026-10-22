using UnityEngine;
using UnityEngine.UI;

public class PausePanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.Pause;

    [Header("Pause Buttons")]
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnPauseRestart;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnQuit;

    private void Start()
    {
        if (_btnResume != null) _btnResume.onClick.AddListener(OnClickResume);
        if (_btnPauseRestart != null) _btnPauseRestart.onClick.AddListener(OnClickRestart);
        if (_btnSettings != null) _btnSettings.onClick.AddListener(OnClickSettings);
        if (_btnQuit != null) _btnQuit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickResume()
    {
        if (GameControler.I != null) GameControler.I.ResumeGame();
        UIManager.I.HidePanel(UIPanelType.Pause);
    }

    private void OnClickRestart()
    {
        if (GameControler.I != null) GameControler.I.RestartGameWithoutReload();
    }

    private void OnClickSettings()
    {
        UIManager.I.HidePanel(UIPanelType.Pause);
        UIManager.I.ShowPanelAdditive(UIPanelType.Settings);
    }

    private void OnClickQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
