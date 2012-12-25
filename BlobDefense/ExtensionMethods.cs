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
        public static PointF Normalize(this PointF pointF)
        {
            var distance = (float)Math.Sqrt((pointF.X * pointF.X) + (pointF.Y * pointF.Y));
            return new PointF(pointF.X / distance, pointF.Y / distance);
        }

        /// <summary>
        /// Normalizes a point, as if it was a vector.
        /// </summary>
        /// <param name="pointF">
        /// The point to normalize.
        /// </param>
        public static void Normalized(this PointF pointF)
        {
            var distance = (float)Math.Sqrt((pointF.X * pointF.X) + (pointF.Y * pointF.Y));
            pointF = new PointF(pointF.X / distance, pointF.Y / distance);
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
        public static void SetMagnitude(this PointF pointF, float magnitude)
        {
            pointF.Normalize();
            pointF.SetXY(pointF.X * magnitude, pointF.X * magnitude);
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
        public static void Add(this PointF pointF, PointF otherPoint)
        {
            pointF.SetXY(pointF.X + otherPoint.X, pointF.Y + otherPoint.Y);
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
        public static void SetX(this PointF pointF, float x)
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
        public static void SetY(this PointF pointF, float y)
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
        public static void SetXY(this PointF pointF, float x, float y)
        {
            pointF = new PointF(x, y);
        }
    }
}
