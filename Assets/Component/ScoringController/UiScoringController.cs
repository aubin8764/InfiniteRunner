using TMPro;
using UnityEngine;

public class UiScoringController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeScoreText;

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


