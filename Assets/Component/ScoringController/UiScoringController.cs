using TMPro;
using UnityEngine;

public class UiScoringController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEventService.OnTimeScoreUpdated += UpdateTimeScore;
    }
    private void UpdateTimeScore(float TimeScore)
    {
        _timeScoreText.text = "Time Survived " + TimeScore.ToString("0");
    }
    private void OnDestroy()
    {
        GameEventService.OnTimeScoreUpdated -= UpdateTimeScore;
    }
}


