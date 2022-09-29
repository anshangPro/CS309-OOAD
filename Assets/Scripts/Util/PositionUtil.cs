using UnityEngine;

namespace Util
{
    public static class PositionUtil
    {
        /// <summary>
        ///   <para>Calculate unit's position when it is standing on the parameter <c>block</c>.</para>
        /// </summary>
        /// <param name="block">The block that unit wants to stand on.</param>
        /// <returns>Position as <c>Vector3</c>.</returns>
        public static Vector3 DstBlock2DstPos3(GameObject block)
        {
            Vector3 newPos = block.transform.position;
            newPos.x -= block.GetComponent<Collider>().bounds.size.x / 2;
            newPos.y += block.GetComponent<Collider>().bounds.size.y;
            newPos.z += block.GetComponent<Collider>().bounds.size.z / 2;
            return newPos;
        }

        public static Vector2 DstBlock2DstPos2(GameObject block)
        {
            Vector3 newPos = DstBlock2DstPos3(block);
            return new Vector2(newPos.x, newPos.z);
        }
    }
}
