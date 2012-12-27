using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    using System.Drawing;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Normalizes a point, as if it was a vector.
        /// </summary>
        /// <param name="pointF">
        /// The point to normalize.
        /// </param>
        /// <returns>
        /// The normalized point.
        /// </returns>
        public static PointF Normalized(this PointF pointF)
        {
            // Do nothing if it is a zero vector
            if(pointF.X == 0 && pointF.Y == 0)
            {
                return pointF;
            }
            
            float distance = (float)Math.Sqrt((pointF.X * pointF.X) + (pointF.Y * pointF.Y));
            return new PointF(pointF.X / distance, pointF.Y / distance);
        }

        /// <summary>
        /// Sets the magnitude/length of a point, as if it was a vector.
        /// </summary>
        /// <param name="pointF">
        /// The point to change magnitude of.
        /// </param>
        /// <param name="magnitude">
        /// The new magnitude/length.
        /// </param>
        public static void SetMagnitude(ref PointF pointF, float magnitude)
        {
            pointF = pointF.Normalized();
            SetXY(ref pointF, pointF.X * magnitude, pointF.Y * magnitude);
        }
        
        /// <summary>
        /// Adds one point to another.
        /// </summary>
        /// <param name="pointF">
        /// The point to add to.
        /// </param>
        /// <param name="otherPoint">
        /// The point to add.
        /// </param>
        public static void Add(ref PointF pointF, PointF otherPoint)
        {
            SetXY(ref pointF, pointF.X + otherPoint.X, pointF.Y + otherPoint.Y);
        }
        
        /// <summary>
        /// Sets the x coordinate of a point.
        /// </summary>
        /// <param name="pointF">
        /// The point object to modify.
        /// </param>
        /// <param name="x">
        /// The new x coordinate.
        /// </param>
        public static void SetX(ref PointF pointF, float x)
        {
            pointF = new PointF(x, pointF.Y);
        }

        /// <summary>
        /// Sets the y coordinate of a point.
        /// </summary>
        /// <param name="pointF">
        /// The point object to modify.
        /// </param>
        /// <param name="y">
        /// The new y coordinate.
        /// </param>
        public static void SetY(ref PointF pointF, float y)
        {
            pointF = new PointF(pointF.Y, y);
        }

        /// <summary>
        /// Sets the x and y coordinate of a point.
        /// </summary>
        /// <param name="pointF">
        /// The point object to modify.
        /// </param>
        /// <param name="x">
        /// The new x coordinate.
        /// </param>
        /// <param name="y">
        /// The new y coordinate.
        /// </param>
        public static void SetXY(ref PointF pointF, float x, float y)
        {
            pointF = new PointF(x, y);
        }
    }
}
