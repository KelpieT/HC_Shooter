using UnityEngine;

public class RangeProjectile : Projectile
{
	[SerializeField] private RangeProjectileData projectileData;
	private bool wasExplode;
	protected override void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case ConstParams.TAG_ENEMY:
			case ConstParams.TAG_GROUND:
				Explode();
				break;
		}
	}

	private void Explode()
	{
		if (wasExplode)
		{
			return;
		}
		Collider[] colliders = Physics.OverlapSphere(transform.position, projectileData.ProjectileRange);
		foreach (var item in colliders)
		{
			IDamageble damageble = null;
			if (!item.TryGetComponent<IDamageble>(out damageble))
			{
				continue;
			}
			damageble.TakeDamage(damage);

		}
		wasExplode = true;
		Destroy(gameObject);
	}
}
