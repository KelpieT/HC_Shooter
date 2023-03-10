using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
	private Vector3 startPos;
	private Vector3 endPos;
	private float speed;
	protected float damage;
	private AnimationCurve traectoryCurveY;
	float startTime;
	float totalTime;
	float distance;
	Vector3 dif;

	public void Init(WeaponData weaponData, Vector3 startPos, Vector3 endPos)
	{
		this.startPos = startPos;
		this.endPos = endPos;
		speed = weaponData.ProjectileSpeed;
		damage = weaponData.ProjectileDamage;
		traectoryCurveY = weaponData.TraectoryCurveY;
		startTime = Time.time;
		distance = Vector3.Distance(startPos, endPos);
		totalTime = distance / speed;

	}

	private void Update()
	{
		float t = (Time.time - startTime) / totalTime;
		if (t <= 1)
		{
			Move(t);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Move(float t)
	{
		Vector3 dif = endPos - startPos;
		Vector3 trayectoryOffsetY = Vector3.up * distance * traectoryCurveY.Evaluate(t);
		Vector3 pos = startPos + dif * t + trayectoryOffsetY;
		transform.position = pos;
	}
	
	protected abstract void OnTriggerEnter(Collider other);

}
