using System;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class AStarAgent : MonoBehaviour
	{
		public Action OnPathComplite;
		public Action OnPathError;
		[SerializeField] private float speedMove;
		[SerializeField] private float tLerpPosition = 10f;
		[SerializeField] private float tLerpRotation = 10f;
		private AStarSeeker seeker;
		private Path path;
		private float traveledDistance;

		public void Init(AStarMap map)
		{
			seeker = new AStarSeeker(map);
		}

		public void GoToPoint(Vector3 position)
		{
			path = seeker.GetPath(transform.position, position);
			path.SetStartPos(transform.position);
			if (path != null)
			{
				StartPath();
			}
		}

		public void SetSpeed(float speed)
		{
			speedMove = speed;
		}

		private void StartPath()
		{
			traveledDistance = 0;
		}
		private void Update()
		{
			if (path == null)
			{
				return;
			}
			traveledDistance += speedMove * Time.deltaTime;
			PathElement pathElement;
			pathElement = path.EvaluateByDistance(traveledDistance);
			if (pathElement == null)
			{
				OnPathError?.Invoke();
				return;
			}
			transform.position = Vector3.Lerp(transform.position, pathElement.position, tLerpPosition * Time.deltaTime);
			Quaternion toRotation = Quaternion.LookRotation(pathElement.direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, tLerpRotation * Time.deltaTime);
			if (path.GetTotalLength() <= traveledDistance)
			{
				OnPathComplite?.Invoke();
			}
			path.DebugDraw();
		}



	}

}