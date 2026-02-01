using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        [SerializeField] private Material _colorLevel;
        public int PlayerLife => _playerLife;
        public float Speed => _speed;
        public Material ColorLevel => _colorLevel;
    }
}
