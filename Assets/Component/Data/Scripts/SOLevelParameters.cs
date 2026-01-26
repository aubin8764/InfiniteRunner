using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;

        public int PlayerLife => _playerLife;
    }
}
