using Default_Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes.Level_Design.TypeBlocks
{
    internal class CastleGroundBlock : Block
    {
        public CastleGroundBlock(Rectangle newrectangle) : base(newrectangle)
        {
            this.isTrigger = false;
            texture = Content.Load<Texture2D>("Textures/TileAssets/CastleGroundTile");
            this.Rectangle = newrectangle;

        }
    }
}
