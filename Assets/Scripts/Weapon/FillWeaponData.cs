using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/FillWeaponData")]
public class FillWeaponData : ScriptableObject
{
	[SerializeField] private float timeFillCage = 1f;
	[SerializeField] private int countFillCage = 1;

	public float TimeFillCage { get => timeFillCage; }
	public int CountFillCage { get => countFillCage; }


}