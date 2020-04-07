using UnityEngine;

namespace Tools
{
    public static class Vector2IntExtensions
    {
        public static Vector3 ToVector3(this Vector2Int v2, float y, int cellsize = 1)
        {
            return new Vector3(v2.x * cellsize, y, v2.y * cellsize);
        }
    }
}
