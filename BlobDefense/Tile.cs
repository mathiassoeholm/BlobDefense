// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a tile, which is a special game object using index position to render instead of pixels.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System.Drawing;

    /// <summary>
    /// Defines a tile, which is a special game object using tile map coordinates to render instead of pixels.
    /// </summary>
    public class Tile : GameObject
    {
        /// <summary>
        /// Renders the tile using tile map coordinates.
        /// </summary>
        /// <param name="x">
        /// The x index from the tile map.
        /// </param>
        /// <param name="y">
        /// The y index from the tile map.
        /// </param>
        /// <param name="context">
        /// The graphics context used to display the tile.
        /// </param>
        public void Render(int x, int y, Graphics context)
        {
            // Set position
            this.Position = new PointF(x * this.SpriteSheetSource.Width, y * this.SpriteSheetSource.Height);
            
            // Render tile
            base.Render(context);
        }
    }
}
