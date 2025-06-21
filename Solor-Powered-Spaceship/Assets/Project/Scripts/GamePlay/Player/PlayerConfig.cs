
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerScriptableObject", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement Setting")]
    public int health;
    public int maxHealth;

    public float maxFuel=100f;
    public float currentFuel;
    public float FuelDrainRate = 1;

    [Header("Movement Setting")]
    public float forwardspeed = 5f;
    public float smoothTime = 0.2f;
    public float TouchSensitivity = 10f;



    [Header("Thrust Varibles")]
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float thrustForce = 5f;
    public float energyDrainRate = 1;
    public float fallSpeed = 5f;
    public bool isThrusting = false;
        

     
  
   
}

