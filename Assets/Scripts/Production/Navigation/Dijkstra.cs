using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Tools;

namespace AI
{
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	public class Dijkstra : IPathFinder
	{
		private List<Vector2Int> _accessibles;

		public Dijkstra(List<Vector2Int> accessibles)
		{
			_accessibles = accessibles;
		}
		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			List<Node> NodeArea = new List<Node>
			{
				new Node(start, 0, GetHCost(start, goal), null)
			};
			List<Node> closestNodes;

			do
			{
				int minFCost = NodeArea.Where(n => !n.NodeCompleted).Min(n => n.FCost);
				closestNodes = NodeArea.FindAll(n => n.FCost == minFCost && !n.NodeCompleted);

				if (!closestNodes.Any())
					break;

				foreach(Node current in closestNodes)
				{
					foreach(Vector2Int direction in DirectionTools.Dirs)
					{
						Vector2Int newNodePos = current.Position + direction;
						if (newNodePos == goal)
						{
							Node lastNode = new Node(newNodePos, GetGCost(newNodePos, start), 0, current);
							return DrawPath(lastNode, start);
						}
						else
						{
							if (_accessibles.Contains(newNodePos) && !NodeArea.Any(n => n.Position == newNodePos))
							{
								Node newNode = new Node(newNodePos, GetGCost(newNodePos, start), GetHCost(newNodePos, goal), current);
								NodeArea.Add(newNode);
							}
						}
					}
					current.NodeCompleted = true;
				}

			} while (NodeArea.Any(n => !n.NodeCompleted));

			return new List<Vector2Int>();
		}

		private IEnumerable<Vector2Int> DrawPath(Node lastNode, Vector2Int start)
		{
			List<Vector2Int> path = new List<Vector2Int>();
			Node current = lastNode;
			while (current.Parent != null)
			{
				path.Add(current.Position);
				current = current.Parent;
			}
			path.Add(start);
			return path;
		}

		private int GetGCost(Vector2Int newNodePosition, Vector2Int start)
		{
			int x = Mathf.Abs(newNodePosition.x - start.x);
			int y = Mathf.Abs(newNodePosition.y - start.y);
			return x + y;
		}

		private int GetHCost(Vector2Int newNodePosition, Vector2Int goal)
		{
			int x = Mathf.Abs(newNodePosition.x - goal.x);
			int y = Mathf.Abs(newNodePosition.y - goal.y);
			return x + y;
		}
	}    
}


