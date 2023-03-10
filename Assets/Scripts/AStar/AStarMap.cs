using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class AStarMap : MonoBehaviour
	{
		private readonly Vector2Int[] selectPatern = new Vector2Int[]
		{

			Vector2Int.up,
			Vector2Int.down,
			Vector2Int.left,
			Vector2Int.right,
			new Vector2Int( 1, -1),
			new Vector2Int( -1, -1),
			new Vector2Int( -1, 1),
			new Vector2Int( 1, 1)
		};
		[SerializeField] private LayerMask positiveLayerMask;
		[SerializeField] private LayerMask negativeLayerMask;
		[SerializeField] private float heightRayCast;
		[SerializeField] private float distanceRayCast;
		[SerializeField] private Vector2Int mapSize;
		[SerializeField] private float nodeSize = 1;
		[SerializeField] private Vector2 centerOffset;
		[SerializeField] private Color walkableColor;
		[SerializeField] private Color nonwalkableColor;

		private Node[,] nodesMap;
		private List<Node> walkableNodes = new List<Node>();

		public void Init()
		{
			Scan();
		}

		[ContextMenu("Scan")]
		public void Scan()
		{
			walkableNodes.Clear();
			nodesMap = new Node[mapSize.x, mapSize.y];
			for (int x = 0; x < mapSize.x; x++)
			{
				for (int y = 0; y < mapSize.y; y++)
				{
					Node node;
					bool isWalkable;
					Vector3 pos;
					RaycastHit raycastHit;
					Vector3 rayOrigin = new Vector3(x * nodeSize + centerOffset.x, heightRayCast, y * nodeSize + centerOffset.y);
					Ray ray = new Ray(rayOrigin, Vector3.down);
					if (Physics.Raycast(ray, out raycastHit, distanceRayCast, negativeLayerMask))
					{
						isWalkable = false;
						pos = raycastHit.point;
						node = new Node(isWalkable, pos);
					}
					else if (Physics.Raycast(ray, out raycastHit, distanceRayCast, positiveLayerMask))
					{
						isWalkable = true;
						pos = raycastHit.point;
						node = new Node(isWalkable, pos);
						walkableNodes.Add(node);
					}
					else
					{
						isWalkable = false;
						pos = new Vector3(x * nodeSize + centerOffset.x, transform.position.y, y * nodeSize + centerOffset.y);
						node = new Node(isWalkable, pos);
					}
					nodesMap[x, y] = node;
				}
			}
			SetNeighbors();
		}
		private void SetNeighbors()
		{
			int xLengh = nodesMap.GetUpperBound(0) + 1;
			int yLengh = nodesMap.GetUpperBound(1) + 1;
			for (int x = 1; x < xLengh - 1; x++)
			{
				for (int y = 1; y < yLengh - 1; y++)
				{
					Node node = nodesMap[x, y];
					if (!node.IsWalkable)
					{
						continue;
					}
					foreach (var item in selectPatern)
					{
						Node nearNode = nodesMap[x + item.x, y + item.y];
						if (!nearNode.IsWalkable)
						{
							continue;
						}
						node.AddToNeighbors(nearNode);
						nearNode.AddToNeighbors(node);
					}
				}
			}
		}

		public Node GetNodeFromWorldPos(Vector3 pos)
		{
			int x = Mathf.RoundToInt((pos.x - centerOffset.x) / nodeSize);
			x = Mathf.Clamp(x, 0, mapSize.x - 1);
			int y = Mathf.RoundToInt((pos.z - centerOffset.y) / nodeSize);
			y = Mathf.Clamp(y, 0, mapSize.y - 1);
			return nodesMap[x, y];
		}

		public List<Node> GetWalkableNodes()
		{
			return walkableNodes;
		}
		public Node GetRandomWalkableNode()
		{
			return walkableNodes.RandomFromLis();
		}

#if UNITY_EDITOR
		void OnDrawGizmos()
		{
			if (nodesMap == null)
			{
				return;
			}
			int xLengh = nodesMap.GetUpperBound(0) + 1;
			int yLengh = nodesMap.GetUpperBound(1) + 1;
			Gizmos.color = Color.cyan;
			for (int x = 1; x < xLengh - 1; x++)
			{
				for (int y = 1; y < yLengh - 1; y++)
				{
					Node node = nodesMap[x, y];
					Gizmos.color = node.IsWalkable ? walkableColor : nonwalkableColor;
					Gizmos.DrawCube(node.Pos, Vector3.one * (nodeSize - 0.1f));
				}
			}

		}
#endif
	}
}

