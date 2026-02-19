using UnityEngine;

public class PlayCollectible : MonoBehaviour
{
    [Header("CoinCollectible Detection")]
    [SerializeField] private Vector3 _sphereCenter;
    [SerializeField] private float _sphereRadius = 1f;
    [SerializeField] private LayerMask _collectibleMask;

    private readonly Collider[] _results = new Collider[3];

    private void Update()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position + _sphereCenter,
            _sphereRadius,
            _results,
            _collectibleMask
        );

        for (int i = 0; i < hitCount; i++)
        {
            var collectibleBase = _results[i].GetComponent<CollectibleBase>();
            if (collectibleBase != null)
            {
                collectibleBase.Collect();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _sphereCenter, _sphereRadius);
    }

}