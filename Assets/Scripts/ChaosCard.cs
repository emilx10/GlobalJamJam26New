using TMPro;
using UnityEngine;

public class ChaosCard : MonoBehaviour
{
    public ChaosCardData data;

    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI sideText;

    void OnEnable()
    {
        mainText.text = data.mainEffectText;
        sideText.text = "Side effect: ???";
    }

    public void RevealSideEffect()
    {
        sideText.text = data.hiddenSideEffectText;
    }
}
