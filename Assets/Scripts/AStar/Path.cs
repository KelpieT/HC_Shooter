using System.Collections.Generic;
using UnityEngine;

namespace AStar
{

	public class Path
	{
		private List<Node> path;
		private float totalLengh;
		private Vector3? startPos = null;

		public Path(List<Node> path)
		{
			this.path = path;
			Calculate();
		}

		public void SetStartPos(Vector3 startPos)
		{
			this.startPos = startPos;
		}

		public PathElement EvaluateByT01(float t)
		{
			t = Mathf.Clamp01(t);
			float curDistance = totalLengh * t;
			return EvaluateByDistance(curDistance);
		}


		public PathElement EvaluateByDistance(float travaledDistance)
		{
			if (path.Count <= 1)
			{
				Debug.LogWarning("Path have one or less nodes");
				return null;
			}
			travaledDistance = Mathf.Clamp(travaledDistance, 0f, totalLengh);
			float curDistance = travaledDistance;
			Vector3 position;
			Vector3 direction;
			float distance = 0;
			float tempDistance = 0;
			for (int i = 0; i < path.Count - 1; i++)
			{
				Node node1 = path[i];
				Node node2 = path[i + 1];
				Vector3 pos1 = node1.Pos;
				if (i == 0 && startPos != null)
				{
					pos1 = (Vector3)startPos;
				}
				Vector3 dif = node2.Pos - pos1;
				float dist = dif.magnitude;
				tempDistance += dist;
				if (curDistance <= tempDistance)
				{
					float distanceBeetwenNodes = curDistance - distance;
					float nodesT = distanceBeetwenNodes / dist;
					position = pos1 + dif * nodesT;
					direction = dif.normalized;
					return new PathElement(position, direction);
				}
				distance = tempDistance;
			}
			return null;
		}

		private void Calculate()
		{
			if (path.Count <= 1)
			{
				Debug.LogWarning("Path have one or less nodes");
				return;
			}
			for (int i = 0; i < path.Count - 1; i++)
			{
				Node node1 = path[i];
				Vector3 pos1 = node1.Pos;
				if (i == 0 && startPos != null)
				{
					pos1 = (Vector3)startPos;
				}
				Node node2 = path[i + 1];
				totalLengh += Vector3.Distance(pos1, node2.Pos);
			}
		}

		public float GetTotalLength()
		{
			return totalLengh;
		}

		public void DebugDraw()
		{
			for (int i = 0; i < path.Count - 1; i++)
			{
				Node node1 = path[i];
				Vector3 pos1 = node1.Pos;
				if (i == 0 && startPos != null)
				{
					pos1 = (Vector3)startPos;
				}
				Node node2 = path[i + 1];
				Debug.DrawLine(pos1, node2.Pos, Color.blue);
			}
		}
	}
}
