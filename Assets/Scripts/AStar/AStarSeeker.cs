using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class AStarSeeker
	{
		private AStarMap map;

		public AStarSeeker(AStarMap map)
		{
			this.map = map;
		}

		public Path GetPath(Vector3 startPos, Vector3 endPos)
		{
			Node start = map.GetNodeFromWorldPos(startPos);
			Node end = map.GetNodeFromWorldPos(endPos);
			return GetPath(start, end);
		}

		public Path GetPath(Node start, Node target)
		{
			List<Node> openSet = new List<Node>();
			List<Node> closedSet = new List<Node>();

			openSet.Add(start);
			while (openSet.Count > 0)
			{
				Node current = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (openSet[i].F < current.F ||
					(openSet[i].F == current.F && openSet[i].H < current.H))
					{
						current = openSet[i];
					}
				}

				openSet.Remove(current);
				closedSet.Add(current);

				if (current == target)
				{
					return new Path(RetracePath(start, target));
				}
				foreach (var item in current.Neighbors)
				{
					if (closedSet.Contains(item))
					{
						continue;
					}
					int newMovementCostToNeighbor = current.G + Node.Distance(current, item);
					if (newMovementCostToNeighbor < item.G || !openSet.Contains(item))
					{
						item.G = newMovementCostToNeighbor;
						item.H = item.Distance(target);
						item.Parent = current;

						if (!openSet.Contains(item))
						{
							openSet.Add(item);
						}
					}
				}
			}
			return null;
		}

		private List<Node> RetracePath(Node start, Node end)
		{
			List<Node> path = new List<Node>();
			Node current = end;
			while (current != start)
			{
				path.Add(current);
				current = current.Parent;
			}
			path.Reverse();
			return path;
		}
	}
}
