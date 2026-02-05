using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(menuName = "Data/CollectibleTemplate")]
    public class CollectibleTemplate : ScriptableObject
    {
        [SerializeField] private GameObject _collectiblePrefab;
        public GameObject CollectiblePrefab => _collectiblePrefab;
    }

    public static class CollectibleCreator
    {
        public static GameObject Create(CollectibleTemplate collectibleTemplate)
        {
            GameObject collectible = Object.Instantiate(collectibleTemplate.CollectiblePrefab);

            return collectible;
        }
    }
}
