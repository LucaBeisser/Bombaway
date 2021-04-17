using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class Goblin : MonoBehaviour, IExplodingElement, IClickable
{

    [SerializeField]
    GameObject destroyedGoblin;

    int pointsForKillingGoblin = -500;

    [Button]
    void Collect ()
    {
        BombsAndGoblinsTracker.Instance.AddGoblin();
        Destroy(gameObject);
    }

    public void Explode(Bomb source)
    {
        Debug.Log("Goblin died through explosion");

        if (destroyedGoblin != null)
            Instantiate(destroyedGoblin, transform.position, Quaternion.identity);

        Score.Instance.Add(pointsForKillingGoblin, ScoreType.Goblin);

        Destroy(gameObject);
    }

    public void Click()
    {
        Collect();
    }
}
