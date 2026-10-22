using UnityEngine;

public class GameOverState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 0f;
        if (UIManager.I != null) UIManager.I.ShowPanelSingle(UIPanelType.GameOver);

        if (MovingFloor.I != null) MovingFloor.I.MoveToTitlePosition();

        if (AudioManager.I != null)
        {
            AudioManager.I.StopMusic();
            AudioManager.I.PlayGameOverStinger();
        }
    }

    public void Tick() { }
    
    public void Exit()
    {
        MovingFloor.I.IsMoving = false;
    }
}