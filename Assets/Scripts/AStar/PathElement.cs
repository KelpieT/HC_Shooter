using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class PathElement
	{
		public Vector3 position;
		public Vector3 direction;

		public PathElement(Vector3 position, Vector3 direction)
		{
			this.position = position;
			this.direction = direction;
		}
	}
}
