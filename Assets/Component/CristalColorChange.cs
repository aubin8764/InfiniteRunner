using UnityEngine;

public class CristalColorChanges : MonoBehaviour
{
    [Header("Cristal Settings")]
    [SerializeField] private int _cristalsRequiredForChange = 3; //  Seuil

    [Header("Debug")]
    [SerializeField] private bool _showDebugLogs = true;

    private int _cristalCount = 0;
    private Material _pendingMaterial; // Matériau en attente

    private void OnEnable()
    {
        GameEventService.OnCristalCollected += OnCristalCollected;
    }

    private void OnDisable()
    {
        GameEventService.OnCristalCollected -= OnCristalCollected;
    }

    private void OnCristalCollected(int value, Material mat)
    {
        _cristalCount += value;
        _pendingMaterial = mat; // Sauvegarder le dernier matériau

        if (_showDebugLogs)
        {
            Debug.Log($" Cristaux : {_cristalCount}/{_cristalsRequiredForChange}");
        }

        //  Changement seulement quand le seuil est atteint
        if (_cristalCount >= _cristalsRequiredForChange)
        {
            Debug.Log("Cristal count = cristal required for change");
            ChangeObstaclesColor(_pendingMaterial);
            _cristalCount = 0; // Reset pour le prochain changement
        }
    }

    private void ChangeObstaclesColor(Material mat)
    {
        if (mat == null)
        {
            Debug.LogError(" Matériau null !");
            return;
        }

        GameEventService.OnObstacleColorChange?.Invoke(mat);

        if (_showDebugLogs)
        {
            Debug.Log($" Couleur changée en '{mat.name}' !");
        }
    }
}