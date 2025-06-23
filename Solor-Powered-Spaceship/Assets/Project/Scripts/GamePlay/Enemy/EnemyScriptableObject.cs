using System;
using UnityEngine;

namespace Solar.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableOnject", menuName = "EnemyScriptableObject/Enemydata")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float initialSpawnRate;
        public float spawnDistance;
        public float miniSpawnRate;
        public float difficultyRate;
        public Enemydata enemydata;
    }

    [Serializable]
    public struct Enemydata
    {
        public int maxHealth;
        public int DamgeOnCollide;
        public Material enemyMaterial;
        public EnemyType enemyType;
    }

    public enum EnemyType
    {
        Destructible,
        Indestructible
    }
}