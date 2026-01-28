using Component.SODB;
using UnityEngine;

public class ScoringController : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private float _scoreMultiplier = 1f; // Ajuste la vitesse du score

    private float _score = 0f;

    private void Update()
    {
        int levelIndex = 1;
        if (!SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        }

        var parameters = ScriptableObjectDataBase.GetByName("Level" + levelIndex);

        // On aigmente le score en fonction de la vitesse du niveau
        _score += parameters.Speed * _scoreMultiplier * Time.deltaTime;

        // On envoie le score à l'UI
        GameEventService.OnScoreUpdated?.Invoke(Mathf.FloorToInt(_score));
    }
}


