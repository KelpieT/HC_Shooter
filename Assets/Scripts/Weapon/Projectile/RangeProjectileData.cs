using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ProjectileData")]
public class RangeProjectileData : ScriptableObject 
{
    [SerializeField] private float projectileRange;
	public float ProjectileRange { get => projectileRange; }
}