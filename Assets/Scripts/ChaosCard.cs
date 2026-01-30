using UnityEngine;
using TMPro;

public class ChaosCard : MonoBehaviour
{
    [Header("Card Text")]
    [TextArea(3, 6)]
    public string cardDescription;

    public TextMeshProUGUI descriptionText;

    void OnEnable()
    {
        descriptionText.text = cardDescription;
    }
}