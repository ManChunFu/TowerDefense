using UnityEngine;

namespace AI
{
    public class Node
    {
        public Node Parent;
        public Vector2Int Position;
        public int GCost;
        public int HCost;
        public int FCost => GCost + HCost;
        public bool NodeCompleted;

        public Node(Vector2Int position, int gCost, int hCost, Node parent)
        {
            Position = position;
            GCost = gCost;
            HCost = hCost;
            Parent = parent;
        }
    }
    
}
