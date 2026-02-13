using System.Collections.Generic;
using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(fileName = "Resources", menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        [SerializeField] private int _cristalPickedToChangeColor = 0;
        [SerializeField] private Material _cristalMaterials;
        [SerializeField] private List<Material> _chunkMaterials;

        [SerializeField] private CollectibleTemplate _cristalTemplate;
        [SerializeField, Range(0, 99)] private int _cristalSpawnChance;

        [SerializeField] private CollectibleTemplate _energySphereTemplate;
        [SerializeField, Range(0, 99)] private int _energySphereSpawnChance;

        public int PlayerLife => _playerLife;
        public float Speed => _speed;
        public int CristalPickedToChangeColor => _cristalPickedToChangeColor;

        public CollectibleTemplate CristalTemplate => _cristalTemplate;
        public int CristalSpawnChance => _cristalSpawnChance;

        public CollectibleTemplate HeartTemplate => _energySphereTemplate;
        public int HeartSpawnChance => _energySphereSpawnChance;

        public Material CristalMaterials => _cristalMaterials;
        public Material GetRandomChunkMaterial() => _chunkMaterials[Random.Range(0, _chunkMaterials.Count)];
    }
}
