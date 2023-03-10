using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject
{
	[SerializeField] private float maxHealth;
	[SerializeField] private float speedMove;
	[SerializeField] private float speedDash;
	[SerializeField] private float minTimeBeetweenDash;
	[SerializeField] private float maxTimeBeetweenDash;
	[SerializeField] private float timeDash;

	public float MaxHealth { get => maxHealth; }
	public float SpeedMove { get => speedMove; }
	public float SpeedDash { get => speedDash; }
	public float MinTimeBeetweenDash { get => minTimeBeetweenDash; }
	public float MaxTimeBeetweenDash { get => maxTimeBeetweenDash; }
	public float TimeDash { get => timeDash; }
}