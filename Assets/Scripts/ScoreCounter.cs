using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour 
{
    private int _score = 0;

    public event Action<int> ScoreChenged;

    public void AddScore(int value) {
        _score += value;
        ScoreChenged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChenged?.Invoke(_score);
    }
}