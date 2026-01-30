using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaosCardUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    ChaosCardData data;

    public void Setup(ChaosCardData card)
    {
        data = card;
        titleText.text = card.title;
        descText.text = card.description;
    }

    public void Choose()
    {
        ChaosManager.Instance.ApplyCard(data);
    }
}