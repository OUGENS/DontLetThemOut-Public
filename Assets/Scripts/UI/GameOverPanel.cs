using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : UIPanel
{
    public override UIPanelType PanelType => UIPanelType.GameOver;

    [Header("Game Over Buttons")]
    [SerializeField] private Button _btnGameOverRestart;
    [SerializeField] private Button _btnQuitGame;

    private void Start()
    {
        if (_btnGameOverRestart != null) _btnGameOverRestart.onClick.AddListener(OnClickRestart);
        if (_btnQuitGame != null) _btnQuitGame.onClick.AddListener(OnClickQuit);
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
