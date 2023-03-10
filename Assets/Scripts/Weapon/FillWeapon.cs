using UnityEngine;

public class FillWeapon : WeaponBase
{
	[SerializeField] private FillWeaponData fillWeaponData;
	[SerializeField] private WeaponUI weaponUI;
	private float lastFillCageTime;

	public override void Init()
	{
		base.Init();
		weaponUI.ShowBulets(CurrentCountCage);
	}

	private void Update()
	{
		FillCage();

	}

	private void FillCage()
	{
		if (CurrentCountCage >= weaponData.MaxCountCage)
		{
			weaponUI.ShowTimer(false);
			return;
		}
		if (Time.time - lastFillCageTime >= fillWeaponData.TimeFillCage)
		{
			weaponUI.ShowTimer(true);
			CurrentCountCage += fillWeaponData.CountFillCage;
			weaponUI.ShowBulets(CurrentCountCage);
			lastFillCageTime = Time.time;
		}
		else
		{
			weaponUI.ShowTimer(true);
			weaponUI.ShowTimer(fillWeaponData.TimeFillCage - (Time.time - lastFillCageTime));
			weaponUI.ShowBulets(CurrentCountCage);
		}
	}

	protected override void Fire(Vector3 targetPos)
	{
		base.Fire(targetPos);
		lastFillCageTime = Time.time;
		weaponUI.ShowBulets(CurrentCountCage);
	}
}
