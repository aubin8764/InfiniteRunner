using Component.Data;
using Components.SODataBase;
using TMPro;
using UnityEngine;

public class UICristalCountController : MonoBehaviour
{
    [SerializeField] private TMP_Text _cristalCountText;

    private void Start()
    {
        int levelIndex = 1;

        if (SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        }
        
        var parameters = ScriptableObjectDataBase.Get<SOLevelParameters>("Level" + levelIndex);


            UpdateCristalCount(parameters.CristalPickedToLevelUp);

            GameEventService.OnCristalCountUpdate += UpdateCristalCount;
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
