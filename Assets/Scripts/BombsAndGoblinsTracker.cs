using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombsAndGoblinsTracker : Singleton<BombsAndGoblinsTracker>
{
    [SerializeField] int scorePerGoblin;

    int goblinsCollected;
    int totalGoblins;
    int bombsLeft;
    int destroyedObjects;

    public event System.Action CollectedAllGoblins;
    public event System.Action GoblinAdded;
    public event System.Action OutOfBombs;

    public int CollectedGoblins { get => Instance.goblinsCollected; }
    public int TotalGoblins { get => Instance.totalGoblins; }
    public int TotalDestroyedObjects { get => Instance.destroyedObjects; }

    protected override void Awake()
    {
        base.Awake();
        totalGoblins = GameObject.FindObjectsOfType<Goblin>().Length;
        goblinsCollected = 0;
    }

    public void AddGoblin()
    {
        if (Instance == null)
        {
            return;
        }

        Instance.goblinsCollected++;
        Score.Instance.Add(Instance.scorePerGoblin, ScoreType.Goblin);

        GoblinAdded?.Invoke();

        if (Instance.totalGoblins <= Instance.goblinsCollected)
        {
            CollectedAllGoblins?.Invoke();
        }

    }
    public void AddBomb()
    {
        bombsLeft++;
    }

    public void RemoveBomb()
    {
        bombsLeft--;
        if (bombsLeft <= 0)
        {
            OutOfBombs?.Invoke();
        }
    }

    public void AddDestroyedObject()
    {
        destroyedObjects++;
    }

}
