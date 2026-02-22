using TMPro;
using UnityEngine;

public class UICountdownController : MonoBehaviour
{
    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private TMP_Text _countdownText;

    private void Awake()
    {
        GameEventService.OnCountdownState += HandleCountdownState;
        GameEventService.OnCountdownTick += SetCountdown;
    }

    private void OnDestroy()
    {
        GameEventService.OnCountdownState -= HandleCountdownState;
        GameEventService.OnCountdownTick -= SetCountdown;
    }

    private void HandleCountdownState(bool enterState)
    {
        // Active le compte à rebourd
        _countdownPanel.SetActive(enterState);
    }

    private void SetCountdown(float countdown)
    {
        _countdownText.text = countdown.ToString("0");

        if(countdown < 1)
        {
            _countdownText.text = "GO!";
        }
    }
}