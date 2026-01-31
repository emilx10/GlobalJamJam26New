using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChaosCardUI : MonoBehaviour
{
    public ChaosCardData cardData;

    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI sideEffectText;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnEnable()
    {
        mainText.text = cardData.mainEffectText;
        sideEffectText.text = "Side effect: ???";
    }

    void OnClick()
    {
        ChaosManager.Instance.ApplyCard(cardData);
    }

    public void RevealSideEffect()
    {
        sideEffectText.text = cardData.hiddenSideEffectText;
    }
}
