
using UnityEngine;

namespace Utils
{
    public class RandomExtends
    {
        public static Vector2 OnUnitCircle()
        {
            var angle = Random.value * 2 * Mathf.PI;
            return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        }
    }
}
