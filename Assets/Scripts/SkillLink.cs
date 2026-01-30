using UnityEngine;

public class SkillLink : MonoBehaviour
{
    public GameObject offVisual;
    public GameObject onVisual;

    public void Initialize(SkillNode parentNode)
    {
        offVisual.SetActive(true);
        onVisual.SetActive(false);

        parentNode.OnUnlocked += HandleUnlock;
    }

    private void HandleUnlock(SkillNode node)
    {
        offVisual.SetActive(false);
        onVisual.SetActive(true);

        node.OnUnlocked -= HandleUnlock;
    }
}