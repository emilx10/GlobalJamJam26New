using UnityEngine;

public class SkillLink : MonoBehaviour
{
    public GameObject offVisual;
    public GameObject onVisual;

    public void Hide()
    {
        offVisual.SetActive(false);
        onVisual.SetActive(false);
    }

    public void Initialize(SkillNode parentNode)
    {
        Hide();
        parentNode.OnUnlocked += HandleUnlock;
    }

    public void ShowOff()
    {
        offVisual.SetActive(true);
        onVisual.SetActive(false);
    }

    public void ShowOn()
    {
        offVisual.SetActive(false);
        onVisual.SetActive(true);
    }

    private void HandleUnlock(SkillNode node)
    {
        ShowOn();
        node.OnUnlocked -= HandleUnlock;
    }
}