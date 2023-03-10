using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EnemyZoneData")]
public class EnemyZoneData : ScriptableObject
{
	[SerializeField] private float minTimeSpawn;
	[SerializeField] private float maxTimeSpawn;
	[SerializeField] private int countEnemiesOnStart;

	public float MinTimeSpawn { get => minTimeSpawn; }
	public float MaxTimeSpawn { get => maxTimeSpawn; }
	public int CountEnemiesOnStart { get => countEnemiesOnStart; }
}