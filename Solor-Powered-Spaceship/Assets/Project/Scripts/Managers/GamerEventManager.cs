using System;
using UnityEngine;

public class GamerEventManager
{
    public static event Action<float, float> OnEnergyChange;
    public static void ChangeInSolarEnergy(float currentEnergy, float MaxEnergy)
    {
        OnEnergyChange?.Invoke(currentEnergy, MaxEnergy);
    }
    public static event Action<float, float> OnFuelChange;
    public static void ChangeInFuelEnergy(float currentEnergy, float MaxEnergy)
    {
        OnFuelChange?.Invoke(currentEnergy, MaxEnergy);
    }

    public static event Action OnPlayerDie;
    public static void GameOver()
    {
        OnPlayerDie?.Invoke();
    }

    public static event Action OnGameStarted;
    public static void GameStarted()
    {
        OnGameStarted?.Invoke();
    }
}
