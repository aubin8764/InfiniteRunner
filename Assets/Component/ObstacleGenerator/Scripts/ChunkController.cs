using System;
using Component.Data;
using Components.SODataBase;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _obstacle;
    [SerializeField] private MeshRenderer _chunkMeshRenderer;
    private Renderer _spawnCristalRenderer;
    [SerializeField, Range(0, 99)] private int _spawnChance;

    [SerializeField]
    private SOLevelParameters _parameters;

    public Transform EndAnchor => _endAnchor;

    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        GameEventService.OnChunkColorUpdated += HandleChunkColorUpdated;
        GameEventService.OnCristalColorUpdated += HandleCristalColorUpdated;

        int levelIndex = 1;

        if (SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        }

        SOLevelParameters parameters;


        parameters = ScriptableObjectDataBase.Get<SOLevelParameters>("Level" + levelIndex);

        // Sécurités pour ne pas avoir d'erreur au GameOver.
        if (parameters == null)
        {
            return;
        }
        if (_chunkMeshRenderer == null)
        {
            return;
        }

        _chunkMeshRenderer.material = parameters.GetRandomChunkMaterial();


        if (parameters.CristalSpawnChance != 0)
        {
            bool randomSpawnChance = UnityEngine.Random.Range(0, 100) <= parameters.CristalSpawnChance;

            if (randomSpawnChance)
            {
                CollectibleTemplate template = parameters.CristalTemplate;
                GameObject cristal = CollectibleCreator.Create(template);

                cristal.transform.position = _spawnPoint.position;
                cristal.transform.SetParent(_obstacle.transform);

                _spawnCristalRenderer = cristal.GetComponent<Renderer>();
                _spawnCristalRenderer.material = parameters.GetRandomCristalMaterial();
            }
        }

        if (parameters.HeartSpawnChance != 0 && _spawnCristalRenderer == null)
        {
            bool randdomSpawnChance = UnityEngine.Random.Range(0, 100) <= parameters.HeartSpawnChance;

            if (randdomSpawnChance)
            {
                CollectibleTemplate template = parameters.HeartTemplate;
                GameObject heart = CollectibleCreator.Create(template);

                heart.transform.position = _spawnPoint.position;
                heart.transform.SetParent(_obstacle.transform);
            }
        }
    }

    private void OnDestroy()
    {
        GameEventService.OnChunkColorUpdated -= HandleChunkColorUpdated;
        GameEventService.OnCristalColorUpdated -= HandleCristalColorUpdated;
    }

    private void HandleChunkColorUpdated(Material cristalMaterial)
    {
        if(_spawnCristalRenderer != null)
        {
            _spawnCristalRenderer.material = cristalMaterial;
        }
    }
    private void HandleCristalColorUpdated(Material newChunkMaterial)
    {
        _chunkMeshRenderer.material = newChunkMaterial;
    }
}