using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
	[SerializeField] private EnemyZone enemyZone;
	[SerializeField] private TMP_Text countText;

	public void Init()
	{
		enemyZone.OnCountKillsUpdate += ShowCountKills;
	}

	private void ShowCountKills()
	{
		countText.text = enemyZone.GetCountKills().ToString();
	}
}
