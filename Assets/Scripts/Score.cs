using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScoreType
{
    Goblin,
    Bomb,
    DestroyedObject
}

public class Score : Singleton<Score>
{
    int goblinScore = 0;
    int bombScore = 0;
    int destroyedObjectScore = 0;

    public int FinalScore { get => GetFinalScore(); }
    public int TimeScore { get => (int)(Time.timeSinceLevelLoad/10f)*10; }
    public int GoblinScore { get => goblinScore; }
    public int BombScore { get => bombScore; }
    public int DestroyedObjectScore { get => destroyedObjectScore; }

    private int GetFinalScore()
    {
        return Mathf.Max(0, TimeScore + goblinScore + bombScore + destroyedObjectScore);
    }

    public void Add(int amount, ScoreType type)
    {
        switch (type)
        {
            case ScoreType.Goblin:
                goblinScore += amount;
                break;
            case ScoreType.Bomb:
                bombScore += amount;
                break;
            case ScoreType.DestroyedObject:
                destroyedObjectScore += amount;
                break;
        }
    }

}
