using UnityEngine;
using UnityEngine.UI;

public class TitleSettingsPanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.TitleSettings;

    [Header("Title Settings Buttons")]
    [SerializeField] private Button _btnBacktoTitle;

    private void Start()
    {
        if (_btnBacktoTitle != null) _btnBacktoTitle.onClick.AddListener(OnClickBackToTitle);
    }

    private void OnClickBackToTitle()
    {
        UIManager.I.HidePanel(UIPanelType.TitleSettings);
        UIManager.I.ShowPanelSingle(UIPanelType.Title);
    }
}
