using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour 
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _scoreCounter.ScoreChenged += ChengeText;    
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChenged -= ChengeText;
    }

    private void ChengeText(int currentScore)
    {
        _text.text = currentScore.ToString();
    }
}