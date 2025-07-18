using System;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = ("BulletScriptableObject"), menuName = "BulletScriptableObject/BulletSo")]
public class BulletsScriptableObject : ScriptableObject
{
    public float speed;
    public int damge;
    public float lifeTime;
}
