using Component.Data;
using Component.StateMachine;
using UnityEngine;

public class EnergyeSphere : MonoBehaviour
{
    [SerializeField] GameState HandleCollectiblePicked;

    private void Start()
    {
        //HandleCollectiblePicked();
    }

    private void HandleEnergyPicked()
    {
        GameState.HandleCollectiblePicked();
    }
}