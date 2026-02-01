using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(menuName = "Data/Collectible")]
    public class Collectible : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        public int PlayerLife => _playerLife;
        public float Speed => _speed;
    }
}