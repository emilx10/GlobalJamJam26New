using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeTimerScript : MonoBehaviour
{
    [Header("Skill Tree Timer")]
    [SerializeField] float skillTreeTime = 7f;
    [SerializeField] Image skillTreeTimerSand;
    Coroutine skillTreeTimerRoutine;

    private void OnEnable()
    {
        StartCoroutine(DelayedStartTimer());
    }
    IEnumerator DelayedStartTimer()
    {
        yield return null; // wait ONE frame so Unity finishes enabling UI

        skillTreeTimerSand.fillAmount = 1f;
        skillTreeTimerSand.transform.localScale = Vector3.one;

        StartSkillTreeTimer();
    }
    void StartSkillTreeTimer()
    {
        if (skillTreeTimerRoutine != null)
            StopCoroutine(skillTreeTimerRoutine);

        skillTreeTimerRoutine = StartCoroutine(SkillTreeTimer());
    }

    IEnumerator SkillTreeTimer()
    {
        float t = skillTreeTime;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;

            skillTreeTimerSand.fillAmount = t / skillTreeTime;

            // panic pulse last second
            if (t < 1f)
            {
                float s = 1f + Mathf.Sin(Time.unscaledTime * 20f) * 0.08f;
                skillTreeTimerSand.transform.localScale = Vector3.one * s;
            }

            yield return null;
        }

        skillTreeTimerSand.fillAmount = 0f;
        skillTreeTimerSand.transform.localScale = Vector3.one;

        GameManager.Instance.ExitSkillTree();
    }
}
