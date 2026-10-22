using UnityEngine;

public class TitleState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 0f;

        MovingFloor.I.IsMoving = false;

        if (UIManager.I != null) UIManager.I.ShowPanelSingle(UIPanelType.Title);
        if (MovingFloor.I != null) MovingFloor.I.MoveToTitlePosition();
        if (AudioManager.I != null) AudioManager.I.PlayMenuMusic();
    }
    public void Tick() { }
    public void Exit()
    {
        MovingFloor.I.IsMoving = false;
    }
}