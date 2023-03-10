using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AStar;
using UnityEngine;


public class EnemyZone : MonoBehaviour
{
	public event Action OnCountKillsUpdate;
	[SerializeField] private EnemyZoneData enemyZoneData;
	[SerializeField] private AStarMap map;
	[SerializeField] private Enemy enemyPrefab;
	private int countKills;

	public void Init()
	{
		for (int i = 0; i < enemyZoneData.CountEnemiesOnStart; i++)
		{
			InstantiateEnemy();
		}

	}

	private void InstantiateEnemy()
	{

		Node node = map.GetRandomWalkableNode();
		Enemy enemy = Instantiate<Enemy>(enemyPrefab, node.Pos, Quaternion.identity);
		enemy.Init(map);
		enemy.OnEnemyDead += RunWaitInstantiateEnemy;
		enemy.OnEnemyDead += IncreaseCount;
	}

	private IEnumerator WaitInstantiateEnemy()
	{
		float randomTime = UnityEngine.Random.Range(enemyZoneData.MinTimeSpawn, enemyZoneData.MaxTimeSpawn);
		yield return new WaitForSeconds(randomTime);
		InstantiateEnemy();
	}

	private void RunWaitInstantiateEnemy()
	{
		StartCoroutine(WaitInstantiateEnemy());
	}

	private void IncreaseCount()
	{
		countKills++;
		OnCountKillsUpdate?.Invoke();
	}

	public int GetCountKills()
	{
		return countKills;
	}
}
