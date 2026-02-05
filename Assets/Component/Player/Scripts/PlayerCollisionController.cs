using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Sphere Parameters")]
    [SerializeField] private Vector3 _sphereStandCenter;
    [SerializeField] private float _sphereStandRadius;

    [SerializeField] private Vector3 _sphereShrinkCenter;
    [SerializeField] private float _sphereShrinkRadius;

    [Header("Debug")]
    [SerializeField] private bool _isHit;
    [SerializeField] private Vector3 _sphereCenter;
    [SerializeField] private float _sphereRadius;

    private readonly Collider[] _hitResults = new Collider[1];

    private void Start()
    {
        _sphereCenter = _sphereStandCenter;
        _sphereRadius = _sphereStandRadius;
    }

    private void Update()
    {
        var hitCount = Physics.OverlapSphereNonAlloc(transform.position + _sphereCenter, _sphereRadius, _hitResults);

        if(hitCount > 0 && !_isHit)
        {
            if (_hitResults[0].transform.CompareTag("Cristal"))
            {
                GameEventService.OnCristalPicked?.Invoke();
                Destroy(_hitResults[0].gameObject);
            }
            else if (_hitResults[0].transform.CompareTag("EnergySphere"))
            {
                GameEventService.OnEnergySpherePicked?.Invoke();
                Destroy(_hitResults[0].gameObject);
            }
            else
            {
                GameEventService.OnCollision?.Invoke();
            }

            Debug.Log("Player take damage");

            _isHit = true;
        }
        // Reset is hit flag when no collision is detected.
        else if(hitCount == 0)
        {
            _isHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _sphereCenter, _sphereRadius);
    }

    public void ShrinkCollider(bool shrink)
    {
        if (shrink)
        {
            _sphereCenter = _sphereShrinkCenter;
            _sphereRadius = _sphereShrinkRadius;
        }
        else
        {
            _sphereCenter = _sphereStandCenter; 
            _sphereRadius = _sphereStandRadius;
        }
    }
}
