using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    List<Enemy> activeEnemies = new();
    List<Modifier> globalModifiers = new();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);

        // Apply all existing modifiers
        foreach (var mod in globalModifiers)
            enemy.ApplyModifier(mod);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void ApplyGlobal(Modifier modifier)
    {
        globalModifiers.Add(modifier);

        foreach (var enemy in activeEnemies)
            enemy.ApplyModifier(modifier);
    }

    public void ApplyCurrentModifiers(Enemy enemy)
    {
        foreach (var mod in globalModifiers)
            enemy.ApplyModifier(mod);
    }
}