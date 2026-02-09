using Component.Data;
using Component.SODB;
using TMPro;
using UnityEngine;

public class UICristalCountController : MonoBehaviour
{
    [SerializeField] private TMP_Text _cristalCountText;

    [SerializeField]
    private SOLevelParameters _parameters;


    private void Start()
    {
        SOLevelParameters parameters = _parameters;


        int levelIndex = 1;

        if (SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        
            UpdateCristalCount(parameters.CristalPickedToLevelUp);

            GameEventService.OnCristalCountUpdate += UpdateCristalCount;
        }
    }

    private void UpdateCristalCount(int cristalCount)
    {
            _cristalCountText.text = "Cristals :" + cristalCount;
    }

    private void OnDestroy()
    {
        GameEventService.OnCristalCountUpdate -= UpdateCristalCount;
    }
}
