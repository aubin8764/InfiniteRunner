using UnityEngine;
using TMPro;
using System;

public class UIScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        GameEventService.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        GameEventService.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
