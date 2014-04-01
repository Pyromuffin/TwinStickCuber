using System;
using System.Collections;
using XInputDotNetPure;

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


        public static Vector2 vector(this GamePadThumbSticks.StickValue pad)
        {
            return new Vector2(pad.X, pad.Y);
        }

    }
}
