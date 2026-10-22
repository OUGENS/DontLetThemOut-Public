using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager I { get; private set; }

    private Dictionary<UIPanelType, UIPanel> _panelCache = new();

    private void Awake()
    {
        I = this;

        UIPanel[] foundPanels = FindObjectsByType<UIPanel>(FindObjectsSortMode.None);

        foreach (var panel in foundPanels)
        {
            if (panel != null && !_panelCache.ContainsKey(panel.PanelType))
            {
                _panelCache.Add(panel.PanelType, panel);
            }
            else if (panel != null && _panelCache.ContainsKey(panel.PanelType))
            {
                Debug.LogWarning($"[UIManager] Duplicate panel type found: {panel.PanelType} on GameObject: {panel.gameObject.name}. Skipping duplicate.");
            }
        }

        ShowPanelSingle(UIPanelType.Title);
    }

    public void ShowPanelSingle(UIPanelType type)
    {
        HideAllPanels();
        ShowPanelAdditive(type);
    }

    public void ShowPanelAdditive(UIPanelType type)
    {
        if (_panelCache.TryGetValue(type, out UIPanel panel))
        {
            panel.Show();
        }
    }

    public void HidePanel(UIPanelType type)
    {
        if (_panelCache.TryGetValue(type, out UIPanel panel))
        {
            panel.Hide();
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in _panelCache.Values)
        {
            panel.Hide();
        }
    }
}
