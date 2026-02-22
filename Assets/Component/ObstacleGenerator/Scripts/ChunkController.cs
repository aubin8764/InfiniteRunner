using Component.Data;
using Components.SODataBase;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private Transform _cristalSpawnPoint;
    [SerializeField] private Transform _heartSpawnPoint;
    [SerializeField] private Transform _obstacle;
    [SerializeField] private MeshRenderer _chunkMeshRenderer;
    private Renderer _spawnCristalRenderer;

    [SerializeField]
    private SOLevelParameters _parameters;

    public Transform EndAnchor => _endAnchor;

    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        GameEventService.OnCristalColorUpdated += HandleCristalColorUpdated;
        GameEventService.OnChunkMaterialChanged += ChangeMaterial;

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

        //Debug.Log("Instantie le collectible avec sa prefab de façon aléatoire");

        if (parameters.CristalSpawnChance != 0)
        {
            bool randomSpawnChance = UnityEngine.Random.Range(0, 100) <= parameters.CristalSpawnChance;

            if (randomSpawnChance)
            {
                CollectibleTemplate template = parameters.CristalTemplate;
                GameObject cristal = CollectibleCreator.Create(template);

                cristal.transform.position = _cristalSpawnPoint.position;
                cristal.transform.SetParent(_obstacle.transform);
            }
        }

        if (parameters.HeartSpawnChance != 0 && _spawnCristalRenderer == null)
        {
            bool randdomSpawnChance = UnityEngine.Random.Range(0, 100) <= parameters.HeartSpawnChance;

            if (randdomSpawnChance)
            {
                CollectibleTemplate template = parameters.HeartTemplate;
                GameObject heart = CollectibleCreator.Create(template);

                heart.transform.position = _heartSpawnPoint.position;
                heart.transform.SetParent(_obstacle.transform);
            }
        }
    }

    private void OnDestroy()
    {
        GameEventService.OnCristalColorUpdated -= HandleCristalColorUpdated;
        GameEventService.OnChunkMaterialChanged -= ChangeMaterial;
    }

    private void HandleCristalColorUpdated(Material newChunkMaterial)
    {
        _chunkMeshRenderer.material = newChunkMaterial;
    }

    private void ChangeMaterial(Material newMaterial)
    {
        //Debug.Log($"Chunk {gameObject.name} change de couleur vers {newMaterial.name}");

        if (_chunkMeshRenderer != null)
        {
            _chunkMeshRenderer.material = newMaterial;
        }
        else
        {
            //Debug.LogWarning($"Chunk {gameObject.name} n’a pas de MeshRenderer assigné !");
        }
    }
}