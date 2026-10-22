using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.Title;

    [Header("Title Buttons")]
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnTitleSettings;

    private void Start()
    {
        if (_btnPlay != null) _btnPlay.onClick.AddListener(OnClickPlay);
        if (_btnTitleSettings != null) _btnTitleSettings.onClick.AddListener(OnClickTitleSettings);
    }

    private void OnClickPlay()
    {
        if (GameControler.I != null) GameControler.I.StartGame();

        UIManager.I.ShowPanelSingle(UIPanelType.Gameplay);
    }

    private void OnClickTitleSettings()
    {
        UIManager.I.ShowPanelAdditive(UIPanelType.TitleSettings);
    }
}
