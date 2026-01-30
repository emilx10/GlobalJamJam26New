using UnityEngine;

public class SkillTreeLine : MonoBehaviour
{
    public RectTransform from;
    public RectTransform to;
    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    void Update()
    {
        lr.SetPosition(0, from.localPosition);
        lr.SetPosition(1, to.localPosition);
    }
}