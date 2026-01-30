using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    //[SerializeField] private GameObject _CollectiblePrefab;
    [SerializeField] private List<GameObject> _collectible;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField, Range(0, 99)] private int _spawnChance;

    public Transform EndAnchor => _endAnchor;

    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        if(_spawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= _spawnChance;
            if (randomSpawnChance)
            {
                for (var i = 0; i < _collectible.Count; i++)
                {
                    Instantiate(_collectible[1], _spawnPoint);
                }
            }
        }
    }
}
