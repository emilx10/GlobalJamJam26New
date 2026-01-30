using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    public List<SkillData> skills;
    public List<RectTransform> spawnPoints;
    public SkillNode nodePrefab;

    Dictionary<SkillData, SkillNode> dataToNode = new();
    Dictionary<SkillNode, List<SkillNode>> graph = new();
    Dictionary<SkillNode, SkillLink> nodeToIncomingLink = new();

    void Start()
    {
        BuildNodes();
        BuildGraph();
        SetupInitialPreview();
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