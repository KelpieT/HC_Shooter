using System;
using System.Collections;
using AStar;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
	public event Action OnEnemyDead;
	[SerializeField] private EnemyData enemyData;
	[SerializeField] private AStarAgent agent;
	private AStarMap map;
	private float health;
	private float maxHealth;
	private float timeToNextDash;

	public float Health => health;
	public float MaxHealth => maxHealth;


	public void Init(AStarMap map)
	{
		maxHealth = enemyData.MaxHealth;
		health = maxHealth;
		agent.Init(map);
		agent.SetSpeed(enemyData.SpeedMove);
		StartCoroutine(WaitDash());
		this.map = map;
		SetDestination();
		agent.OnPathComplite += SetDestination;
		agent.OnPathError += SetDestination;
	}

	private void SetDestination()
	{
		Node node = map.GetRandomWalkableNode();
		agent.GoToPoint(node.Pos);
	}

	public void TakeDamage(float damage)
	{
		if (health <= 0)
		{
			return;
		}
		health -= damage;
		if (health <= 0)
		{
			Dead();
		}
	}

	public void Dead()
	{
		agent.OnPathComplite -= SetDestination;
		agent.OnPathError -= SetDestination;

		OnEnemyDead?.Invoke();
		Destroy(gameObject);
	}

	private IEnumerator WaitDash()
	{
		float randomTime = UnityEngine.Random.Range(enemyData.MinTimeBeetweenDash, enemyData.MaxTimeBeetweenDash);
		yield return new WaitForSeconds(randomTime);
		agent.SetSpeed(enemyData.SpeedMove);
		StartCoroutine(StartDash());

	}
	private IEnumerator StartDash()
	{
		agent.SetSpeed(enemyData.SpeedDash);
		yield return new WaitForSeconds(enemyData.TimeDash);
		agent.SetSpeed(enemyData.SpeedMove);
		StartCoroutine(WaitDash());
	}

}
