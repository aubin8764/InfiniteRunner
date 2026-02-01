using System.Collections.Generic;
using UnityEngine;
using Component.Data;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    public List<GameObject> _collectible;
    public List<Transform> _spawnPoints;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField, Range(0, 99)] private int _spawnChance;

    private ScoringController scoreController;
    [SerializeField] private float _distanceColorChange;
    [SerializeField] private GameObject _colorFragment;
    [SerializeField] private Transform _spawnPointColorFragment;
    private ChunkController chunkController;

    private SOLevelParameters _colorLevel;

    [SerializeField] private SOLevelParameters material;

    public Transform EndAnchor => _endAnchor;

    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        if (_spawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= _spawnChance;
            if (randomSpawnChance)
            {
                for (var i = 0; i < _collectible.Count; i++)
                {
                    Instantiate(_collectible[i], _spawnPoints[i]);
                }
            }
        }
    }

    private void Update()
    {
        if (scoreController._score == _distanceColorChange)
        {
            Instantiate(_colorFragment, _spawnPointColorFragment);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        chunkController.GetComponent<ChunkController>().material = _colorLevel;
        Destroy(_colorFragment);
    }
}