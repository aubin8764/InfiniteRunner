using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate random chunks and translate them.
/// </summary>

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Tooltip("Translation in m/s")] private float _translationSpeed = 1f;
    [SerializeField] private int _activeChunksCount = 5;
    [SerializeField] private int _behindChunksCount = 2;
    [SerializeField] private bool _preventSameChunkGeneration = true;

    [Header("Prefabs")]
    [SerializeField] private ChunkController[] _chunkPrefabs;

    private readonly List<ChunkController> _activeChunks = new();
    private ChunkController LastChunk => _activeChunks[_activeChunks.Count - 1];

    private int _LastChunkIndex = 0;

    private void Start()
    {
        AddBaseChunks();

        GameEventService.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _translationSpeed = 0f;
    }

    private void AddBaseChunks()
    {
        for(int i = 0; i < _activeChunksCount; i++)
        {
            // First chunk is alxays at the origin.
            if (i == 0)
            {
                AddChunk(Vector3.zero);
                continue;
            }

            AddChunk(LastChunk.EndAnchor.position);
        }
    }

    private void AddChunk(Vector3 position)
    {
        // Avoid generating the same chunk twice in a raw.
        var newChunkIndex = Random.Range(0, _chunkPrefabs.Length);

        if(_preventSameChunkGeneration)
        {
            for (int i = 0; i < 10; i++)
            {
                if (newChunkIndex == _LastChunkIndex)
                {
                    newChunkIndex = Random.Range(0, _chunkPrefabs.Length);
                }
            }
            _LastChunkIndex = newChunkIndex;
        }

        ChunkController chunk = Instantiate(_chunkPrefabs[newChunkIndex], transform);
        chunk.transform.position = position;
        _activeChunks.Add(chunk);
    }

    private void Update()
    {
        foreach (var chunk in _activeChunks)
        {
            chunk.transform.Translate(Vector3.back * _translationSpeed * Time.deltaTime);
        }

        UpdateChunks();
    }

    private void UpdateChunks()
    {
        List<ChunkController> behindChunks = new();

        foreach(var chunk in _activeChunks)
        {
            if(chunk.IsBehind)
            {
                behindChunks.Add(chunk);
            }
        }

        if(behindChunks.Count > _behindChunksCount)
        {
            int chunkToDelateCount = behindChunks.Count - _behindChunksCount;

            for(int i = 0; i < chunkToDelateCount; i++)
            {
                var chunkToDelate = behindChunks[i];
                _activeChunks.Remove(chunkToDelate);
                Destroy(chunkToDelate.gameObject);
            }
        }

        int missingChunkCount = _activeChunksCount - _activeChunks.Count;
        for(int i = 0;i < missingChunkCount;i++)
        {
            AddChunk(LastChunk.EndAnchor.position);
        }
    }
}
