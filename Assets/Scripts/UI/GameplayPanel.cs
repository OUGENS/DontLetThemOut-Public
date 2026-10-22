using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.Gameplay;

    [Header("Gameplay Buttons")]
    [SerializeField] private Button _btnPause;

    private void Start()
    {
        if (_btnPause != null) _btnPause.onClick.AddListener(OnClickPause);
    }

    private void OnClickPause()
    {
        UIManager.I.ShowPanelAdditive(UIPanelType.Pause);

        if (GameControler.I != null)
        {
            GameControler.I.PauseGame();
        }
    }
}
