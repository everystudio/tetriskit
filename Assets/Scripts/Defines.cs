using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class Defines : MonoBehaviour
    {
        public static readonly int GridHeightMax = 25;
        public static readonly int GridWidthMax = 10;

        public static Vector2 roundVec2(Vector2 v)
        {
            return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
        }

    }
}
