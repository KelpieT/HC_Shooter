using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public enum FireStatus
	{
		Success,
		EmptyCage,
		Reload
	}

	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] protected WeaponData weaponData;
	private float lastFireTime;
	private int currentCountCage;

	public int CurrentCountCage
	{
		get => currentCountCage;
		set => currentCountCage = Mathf.Clamp(value, 0, weaponData.MaxCountCage);
	}
	public WeaponData WeaponData { get => weaponData; }

	public virtual void Init()
	{
		CurrentCountCage = weaponData.MaxCountCage;
	}

	public FireStatus TryFire(Vector3 targetPos)
	{
		bool readyToShoot = Time.time - lastFireTime >= weaponData.TimeBeetwenShots;
		if (CurrentCountCage > 0 && readyToShoot)
		{
			Fire(targetPos);
			return FireStatus.Success;
		}
		else if (CurrentCountCage <= 0)
		{
			return FireStatus.EmptyCage;
		}
		else
		{
			return FireStatus.Reload;
		}
	}

	protected virtual void Fire(Vector3 targetPos)
	{
		lastFireTime = Time.time;
		CurrentCountCage--;
		Projectile projectile = Instantiate<Projectile>(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
		projectile.Init(weaponData, spawnPoint.position, targetPos);
	}
}
