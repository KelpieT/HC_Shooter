using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class Node
	{
		private const int multyplyer = 10;
		
		public int H;
		public int G;
		public int F => H + G;

		private bool isWalkable;
		private Vector3 pos;
		private List<Node> neighbors = new List<Node>();
		

		private Node parent;
		public bool IsWalkable { get => isWalkable; }
		public IEnumerable<Node> Neighbors { get => neighbors; }
		public Vector3 Pos { get => pos; }
		public Node Parent { get => parent; set => parent = value; }

		public Node(bool isWalkable, Vector3 pos)
		{
			this.isWalkable = isWalkable;
			this.pos = pos;
		}

		public void AddToNeighbors(Node node)
		{
			if (!neighbors.Contains(node))
			{
				neighbors.Add(node);
			}
		}

		public int Distance(Node to)
		{
			return (int)(Vector3.Distance(Pos, to.Pos) * multyplyer);
		}
		public static int Distance(Node from, Node to)
		{
			return (int)(Vector3.Distance(from.Pos, to.Pos) * multyplyer);
		}
	}
}
