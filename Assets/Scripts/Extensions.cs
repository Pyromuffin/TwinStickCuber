using System;
using System.Collections;

namespace UnityEngine
{
    public static class Extensions
    {
        public static void Times(this int i, Action<int> action)
        {
            for (int j = 0; j < i; j++)
            {
                action(j);
            }
        }

        public static Vector3 randomPoint(this Bounds b)
        {
            return b.center + Vector3.Scale(b.extents, new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f) ,Random.Range(-1.0f, 1.0f) ));
        }


    }
}
