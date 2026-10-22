using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.Settings;

    [Header("Settings Buttons")]
    [SerializeField] private Button _btnBacktoPause;

    private void Start()
    {
        if (_btnBacktoPause != null) _btnBacktoPause.onClick.AddListener(OnClickBackToPause);
    }

    private void OnClickBackToPause()
    {
        UIManager.I.HidePanel(UIPanelType.Settings);
        UIManager.I.ShowPanelAdditive(UIPanelType.Pause);
    }
}
