using AStar;
using UnityEngine;

public class MainEnterPoint : MonoBehaviour
{
	[SerializeField] private AStarMap map;
	[SerializeField] private EnemyZone enemyZone;
	[SerializeField] private WeaponOwner weaponOwner;
	[SerializeField] private EnemyCounter enemyCounter;

	private void Awake()
	{
		map.Init();
		enemyZone.Init();
		weaponOwner.Init();
		enemyCounter.Init();
	}
}
