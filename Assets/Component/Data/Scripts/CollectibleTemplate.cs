using UnityEngine;

namespace Component.Data
{
    [CreateAssetMenu(menuName = "Resources/CollectibleTemplate")]
    public class CollectibleTemplate : ScriptableObject
    {
        [SerializeField] private GameObject _collectiblePrefab;
        public GameObject CollectiblePrefab => _collectiblePrefab;
    }

    public static class CollectibleCreator
    {
        public static GameObject Create(CollectibleTemplate collectibleTemplate)
        {
            //Debug.Log("Instantiation collectible prefab");
            GameObject collectible = Object.Instantiate(collectibleTemplate.CollectiblePrefab);

            return collectible;
        }
    }
}
