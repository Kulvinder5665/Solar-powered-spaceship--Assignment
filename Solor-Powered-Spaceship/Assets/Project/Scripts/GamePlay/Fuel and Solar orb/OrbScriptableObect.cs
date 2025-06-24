

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbScriptableObject", menuName = "OrbScriptableObject/Orbdata")]
public class OrbScriptableObect : ScriptableObject
{
    public OrbData orbData;
    public float initialSpawnRate;
}

[Serializable]
public struct OrbData
{
    public float refillAmount;
    public OrbType orbType;

}

public enum OrbType {
    fuelOrb,
    solarOrb
}
