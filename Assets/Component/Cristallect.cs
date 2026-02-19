using Component.Data;
using UnityEngine;

public class Cristallect : CollectibleBase
{
    [SerializeField] private int _value = 1;
    [SerializeField] private Renderer _renderer;

    protected override void OnCollect()
    {
        Material mat = _renderer.material;
        Debug.Log($"Cristal collecté ! Valeur : {_value}, Matériau : {mat.name}");
        GameEventService.OnCristalCollected?.Invoke(_value, mat);
    }
}