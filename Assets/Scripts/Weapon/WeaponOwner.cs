using System;
using UnityEngine;

public class WeaponOwner : MonoBehaviour
{
	private const int MaxDistanceRay = 100;
	private const int DistancePlane = 40;

	public event Action OnEmptyCage;

	[SerializeField] private WeaponBase currentWeapon;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private Camera cam;

	private InputControl inputControl;

	public void Init()
	{
		currentWeapon.Init();
		inputControl = new InputControl();
		inputControl.Init();
		inputControl.onTouch += TryFire;
		inputControl.onMove += TryFire;

	}

	private void OnDestroy()
	{
		inputControl.onTouch -= TryFire;
		inputControl.onMove -= TryFire;
		inputControl = null;
	}

	private void TryFire(Vector2 screenPos)
	{
		if (currentWeapon == null)
		{
			Debug.Log("weapon null");
			return;
		}
		Vector3 endPos = GetTargetPosition(screenPos);
		WeaponBase.FireStatus status = currentWeapon.TryFire(endPos);
		switch (status)
		{
			case WeaponBase.FireStatus.EmptyCage:
				OnEmptyCage?.Invoke();
				break;
		}

	}

	private Vector3 GetTargetPosition(Vector2 screenPos)
	{
		Ray ray = cam.ScreenPointToRay(screenPos);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, MaxDistanceRay, layerMask))
		{
			return raycastHit.point;
		}
		Plane plane = new Plane(-transform.forward, transform.position + transform.forward * DistancePlane);
		float distance;
		plane.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

}
