using UnityEngine;

public class PauseState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 0f;
        if (UIManager.I != null) UIManager.I.ShowPanelSingle(UIPanelType.Pause);
    }

    public void Tick()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    public void Exit()
    {
        if (UIManager.I != null) UIManager.I.HidePanel(UIPanelType.Pause);
    }
}