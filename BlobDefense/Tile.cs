namespace BlobDefense
{
    using System.Drawing;

    public class Tile : GameObject
    {
        public Tile()
        {
            // Add this kind of tile to the list of tiles.
            TileEngine.Instance.AddTileType(this);
        }

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
