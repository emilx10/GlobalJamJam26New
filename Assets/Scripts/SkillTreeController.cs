using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour
{
    public List<SkillData> skills;
    public List<RectTransform> spawnPoints;
    public SkillNode nodePrefab;

    Dictionary<SkillData, SkillNode> dataToNode = new();
    Dictionary<SkillNode, List<SkillNode>> graph = new();
    Dictionary<SkillNode, SkillLink> nodeToIncomingLink = new();

    [Header("Skill Tree Timer")]
    [SerializeField] float skillTreeTime = 5f;
    [SerializeField] Image skillTreeTimerSand;

    void Start()
    {
        BuildNodes();
        BuildGraph();
        SetupInitialPreview();
        skillTreeTimerSand.fillAmount = 1f;
    }
    private void OnEnable()
    {
        skillTreeTimerSand.fillAmount = 1f;
        StartSkillTreeTimer();
    }
    void StartSkillTreeTimer()
    {
        if (skillTreeTimerSand != null)
        {
            skillTreeTimerSand.fillAmount = 1f;
            // Reset scale in case it was left mid-pulse from the last run
            skillTreeTimerSand.transform.localScale = Vector3.one;
        }

        // Safety: ensure time is greater than 0 to avoid division by zero
        if (skillTreeTime <= 0) skillTreeTime = 5f;

        StopAllCoroutines(); // Prevent multiple timers from running at once
        StartCoroutine(SkillTreeTimer());
    }

    IEnumerator SkillTreeTimer()
    {
        float t = skillTreeTime;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;

            if (skillTreeTimerSand != null)
            {
                // This line specifically controls the visual "going down"
                skillTreeTimerSand.fillAmount = Mathf.Clamp01(t / skillTreeTime);

                // Pulsing effect when less than 1 second remains
                if (t < 1f)
                {
                    float s = 1f + Mathf.Sin(Time.unscaledTime * 20f) * 0.08f;
                    skillTreeTimerSand.transform.localScale = Vector3.one * s;
                }
            }
            yield return null;
        }

        // Ensure it hits exactly 0 at the end
        if (skillTreeTimerSand != null) skillTreeTimerSand.fillAmount = 0f;

        GameManager.Instance.ExitSkillTree();
    }
    void BuildNodes()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            SkillData data = skills[i];
            RectTransform point = spawnPoints[i];

            SkillNode node = Instantiate(nodePrefab, point);
            node.cost = data.cost;
            node.effect = data.effect;
            node.OnUnlocked += OnNodeUnlocked;
            node.gameObject.SetActive(false);
            node.ChanggeData(data);

            SkillLink link = point.GetComponentInChildren<SkillLink>();
            if (link != null)
            {
                link.Hide();
                nodeToIncomingLink[node] = link;
            }

            dataToNode[data] = node;
            graph[node] = new List<SkillNode>();
        }
    }

    void BuildGraph()
    {
        foreach (var data in skills)
        {
            if (data.requiresSkill == null) continue;

            SkillNode parent = dataToNode[data.requiresSkill];
            SkillNode child = dataToNode[data];

            graph[parent].Add(child);

            if (nodeToIncomingLink.TryGetValue(child, out SkillLink link))
            {
                link.Initialize(parent);
            }
        }
    }

    void SetupInitialPreview()
    {
        foreach (var data in skills)
        {
            if (data.requiresSkill != null) continue;

            SkillNode root = dataToNode[data];
            root.Show(true);

            foreach (var child in graph[root])
            {
                child.Show(false);
                if (nodeToIncomingLink.TryGetValue(child, out SkillLink link))
                {
                    link.ShowOff();
                }
            }
        }
    }

    void OnNodeUnlocked(SkillNode unlockedNode)
    {
        foreach (var child in graph[unlockedNode])
        {
            child.Show(true);

            if (graph.ContainsKey(child))
            {
                foreach (var grandChild in graph[child])
                {
                    if (nodeToIncomingLink.TryGetValue(grandChild, out SkillLink nextLine))
                    {
                        nextLine.ShowOff();
                        
                    }
                }
            }
        }
    }
}