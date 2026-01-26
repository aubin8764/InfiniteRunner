using UnityEngine;
using TMPro;
using Component.Data;

public class UILifeController : MonoBehaviour
{
    [SerializeField] private SOLevelParameters _levelParameters;
    [Header("UI References")]
    [SerializeField] private TMP_Text _lifeText;

    private void Start()
    {
        SetLife(_levelParameters.PlayerLife);
        GameEventService.OnPlayerLifeUpdated += SetLife;
    }

    private void OnDestroy()
    {
        GameEventService.OnPlayerLifeUpdated -= SetLife;
    }

    private void SetLife(int life)
    {
        _lifeText.text = "Life : " + life;
    }
}
