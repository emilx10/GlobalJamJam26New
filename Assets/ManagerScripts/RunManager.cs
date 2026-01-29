using TMPro;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public float runDuration = 40f;
    float timer;

    public TextMeshProUGUI timerText;
    bool chaosTriggered;

    public void StartRun()
    {
        timer = runDuration;
        chaosTriggered = false;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 20f && !chaosTriggered)
        {
            chaosTriggered = true;
            GameManager.Instance.EnterChaos();
        }

        if (timer <= 0f)
        {
            GameManager.Instance.EnterSkillTree();
        }
    }
}