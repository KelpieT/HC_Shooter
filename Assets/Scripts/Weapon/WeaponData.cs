using UnityEngine;

[CreateAssetMenu(menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject
{
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float projectileDamage;
	[SerializeField] private int maxCountCage = 5;
	[SerializeField] private float timeBeetwenShots = 0.5f;
	[SerializeField] private AnimationCurve traectoryCurveY;
	
	public float ProjectileSpeed { get => projectileSpeed; }
	public float ProjectileDamage { get => projectileDamage; }
	public int MaxCountCage { get => maxCountCage; }
	public float TimeBeetwenShots { get => timeBeetwenShots; }
	public AnimationCurve TraectoryCurveY { get => traectoryCurveY; }
}