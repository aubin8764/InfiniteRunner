using System;
using Component.Data;
using UnityEngine;

public static class GameEventService
{
    #region GAMEPLAY

    public static Action OnCollision; 
    public static Action OnCristalPicked;
    public static Action OnEnergySpherePicked;
    public static Action<int> OnPlayerLifeUpdated;
    public static Action<float> OnCountdownTick;

    public static Action<int, Material> OnCristalCollected;
    public static Action<Material> OnObstacleColorChange;

    public static Action<Material> OnChunkColorUpdated;
    public static Action<Material> OnCristalColorUpdated;

    public static Action<int> OnCristalCountUpdate;

    public static Action<float> OnTimeScoreUpdated;
    public static Action<float> OnSpeedUpdated;

    #endregion

    public static Action<int> OnScoreUpdated;
    
    public static Action<bool> OnCountdownState;
    public static Action<bool> OnGameState;
    public static Action<bool> OnGameOverState;

    public static Action<SOLevelParameters> OnLevelParametersUpdated;
}