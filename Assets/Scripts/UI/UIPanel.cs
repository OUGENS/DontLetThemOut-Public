using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public abstract UIPanelType PanelType { get; }

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}

public enum UIPanelType
{
    Title,
    Gameplay,
    Pause,
    Settings,
    TitleSettings,
    Win,
    GameOver
}