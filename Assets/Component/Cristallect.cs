using UnityEngine;

public class Cristallect : CollectibleBase
{
    [SerializeField] private int _value = 1;
    [SerializeField] private Renderer _renderer;

    protected override void OnCollect()
    {
        //Debug.Log("éxécuté si cristal entre en collision avec le player");
        Material mat = _renderer.material;
        //Debug.Log($"Cristal collecté ! Valeur : {_value}, Matériau : {mat.name}");
        GameEventService.OnCristalCollected?.Invoke(_value, mat);
        
    }
}