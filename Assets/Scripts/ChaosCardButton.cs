using UnityEngine;
using UnityEngine.UI;

public class ChaosCardButton : MonoBehaviour
{
    public ChaosCardData cardData;
    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => ChaosManager.Instance.ApplyCard(cardData));
    }
}